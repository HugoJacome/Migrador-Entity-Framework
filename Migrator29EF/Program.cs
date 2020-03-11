using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Migrator29EF
{
    public class Program
    {
        public static int clientsQuantity = 0, cardsQuantity = 0, accountsQuantity = 0, cardsTotalQuantity = 0, BranchQuantity = 0, OrderQuantity = 0;
        public static string path = @"C:\Migracion\general.csv";
        public static string pathClientes = @"C:\Migracion\cliente.csv";
        public static string pathCards = @"C:\Migracion\cards.csv";
        public static string pathCards1 = @"C:\Migracion\cards1.csv";
        public static string pathCards2 = @"C:\Migracion\cards2.csv";
        public static string pathAccounts = @"C:\Migracion\accounts.csv";
        public static int[] states = new int[] { 1, 2, 3, 4, 6, 8 };

        public static void Main(string[] args)
        {
            try
            {
                var dbEncConfig = ConfigurationManager.AppSettings["AzureKeyVault"].ToString();
                var fields = dbEncConfig.Split(';').Select(f =>
                {
                    var fs = f.Split(',');
                    return (fs[0], fs[1], fs[2]);
                }).Where(f => f.Item1 == "KV").Select(fs => (fs.Item2, fs.Item3)).ToArray();

                if (fields.Length > 0)
                {
                    InitializeAzureKeyVaultProvider(fields);
                }

                Migration migrar = new Migration();

                writeGeneral("Inicia migracion: ;" + DateTime.Now);
                Console.WriteLine("Inicia migracion: ;" + DateTime.Now);
                //migrar.MigraAgencias();
                //writeGeneral("Agencias migrados: ;" + BranchQuantity + ";Tiempo:  ;" + DateTime.Now);
                //Console.WriteLine("Agencias migrados: ;" + BranchQuantity + ";Tiempo:  ;" + DateTime.Now);
                //migrar.MigrarOrdenes();
                //writeGeneral("Ordenes migrados: ;" + OrderQuantity + ";Tiempo:  ;" + DateTime.Now);
                //Console.WriteLine("Ordenes migrados: ;" + OrderQuantity + ";Tiempo:  ;" + DateTime.Now);
                migrar.MigraClientes29();
                //writeGeneral("Clientes migrados: ;" + clientsQuantity + ";Tiempo:  ;" + DateTime.Now);
                //Console.WriteLine("Clientes migrados: ;" + clientsQuantity + ";Tiempo:  ;" + DateTime.Now);
                //migrar.MigraTarjetasOrden29();
                //writeGeneral("Tarjetas migradas: ;" + cardsQuantity + ";Tiempo:  ;" + DateTime.Now);
                //migrar.MigraCuentasTarjeta29();
                //writeGeneral("Cuentas migradas: ;" + accountsQuantity + ";Tiempo:  ;" + DateTime.Now);
                writeGeneral("Finaliza migracion: ;" + DateTime.Now);
                Console.WriteLine("Finaliza migracion: ;" + DateTime.Now);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar la aplicación", ex);
            }
        }
        #region AzureKeyVault
        private static ClientCredential _clientCredential;

        static void InitializeAzureKeyVaultProvider((string, string)[] apps)
        {

            Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>();

            foreach (var app in apps)
            {
                _clientCredential = new ClientCredential(app.Item1, app.Item2);
                SqlColumnEncryptionAzureKeyVaultProvider azureKeyVaultProvider =
                    new SqlColumnEncryptionAzureKeyVaultProvider(GetToken);
                providers.Add(SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider);
            }
            SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
        }
        public async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, _clientCredential);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the access token");
            return result.AccessToken;
        }

        #endregion AzureKeyVault

        public class Migration
        {
            private CardMasterEntities context = new CardMasterEntities();
            private CardDBEntities newContext = new CardDBEntities();
            private phiAdminEntities phiAdminContext = new phiAdminEntities();          

            int institutionID = Convert.ToInt32(ConfigurationManager.AppSettings["InstitutionID"]);
            
            #region 29Oct
            public void MigraAgencias()
            {
                List<Branches> branches = new List<Branches>();

                var agencias = context.AGENCIA
                    .ToList();

                foreach (var ag in agencias)
                {
                    Branches branch = new Branches
                    {
                        InstitutionId = institutionID,
                        State = (ag.AGE_ESTADO == "A") ? true : false,
                        Zone = ag.AGE_ZONA,
                        Name = ag.AGE_DESCRIPCION,
                        Address = ag.AGE_UBICACION,
                        CoreIdentifier = ag.AGE_CODIGO.ToString()
                    };
                    branches.Add(branch);
                }
                try
                {
                    phiAdminContext.Branches.AddRange(branches);
                    phiAdminContext.SaveChanges();
                    BranchQuantity = branches.Count();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error Agencia");
                    throw;
                }

            }

            public void MigrarOrdenes()
            {
                List<Orders> Orders = new List<Orders>();

                var Ordenes = context.ORDEN
                    .ToList();

                foreach (var orden in Ordenes)
                {
                    Orders order = new Orders
                    {
                        ProfileId = 1,
                        PrefixId = 1,
                        State = 3,
                        InitialSequential = 0,
                        Quantity = orden.ORD_TOTAL,
                        CreationDate = orden.ORD_FECHA_CREACION,
                        ClosingDate = orden.ORD_FECHA_CIERRE,
                        UserName = "MIGRACION"
                    };
                    Orders.Add(order);
                }
                try
                {
                    newContext.Orders.AddRange(Orders);
                    newContext.SaveChanges();
                    OrderQuantity = Orders.Count;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Agencia");
                    throw;
                }
            }

            public void MigraClientes29()
            {
                Clients clientNew = new Clients();
                List<Clients> clientsNew = new List<Clients>();
                var list = new List<List<Clients>>();

                int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };

                var clientes = context.CLIENTE.ToList();

                var branches = phiAdminContext.Branches.Where(branch => branch.InstitutionId == institutionID).ToList();

                var clientesInValidos = context.CLIENTE
                    .Join(context.TARJETA_CUENTA,
                    c => c.CLI_CODIGO,
                    tc => tc.CLI_CODIGO,
                    (c, tc) => new { TarId = tc.TAR_ID, Identificacion = c.CLI_IDENTIFICACION, Name = c.CLI_NOMBRE })
                    .Join(context.TARJETA,
                    t => t.TarId,
                    tar => tar.TAR_ID,
                    (t, tar) => new { t.TarId, t.Identificacion, t.Name, State = tar.EST_TAR_CODIGO, Branch = tar.AGE_CODIGO })
                    .Where(c => states.Contains(c.State))
                    .Select(cliente => new
                    {
                        ID = cliente.Identificacion,
                        Nombre = cliente.Name,
                        BranchId = cliente.Branch
                    })/*.Take(100)*/
                    .ToList();

                var clientsOld = clientesInValidos.GroupBy(cli => cli.ID)
                    .Select(grp => grp.First())
                    .ToList();


                int countClientesOld = clientsOld.Count();      
                using (var file = File.CreateText(pathClientes))
                {
                    foreach (var cli in clientsOld)
                    {
                        clientNew = new Clients()
                        {
                            Name = cli.Nombre,
                            Identification = cli.ID,
                            AgencyId = branches.Where(branch => branch.CoreIdentifier == cli.BranchId.ToString()).Select(branch => branch.Id).FirstOrDefault(),
                            IdentityType = (cli.ID.Length == 10) ? "C" : "R"
                        };
                        clientsNew.Add(clientNew);
                        file.WriteLine(clientNew.Identification + ";" + clientNew.Name);
                    }
                    int valorDivision = 5000;
                    for (int i = 0; i < clientsNew.Count; i += valorDivision)
                    {
                        list.Add(clientsNew.GetRange(i, Math.Min(valorDivision, clientsNew.Count - i)));
                    }
                }
                try
                {
                    foreach (var item in list)
                    {
                        Console.WriteLine("Inicia intervalo: ;" + DateTime.Now);
                        DateTime inicio = DateTime.Now;
                        newContext.Clients.AddRange(item);
                        newContext.SaveChanges();
                        clientsQuantity += item.Count();
                        DateTime termina = DateTime.Now;
                        double tiempo = (termina - inicio).TotalMinutes;
                        Console.WriteLine("Termina invervalo: ;" + DateTime.Now);
                        Console.WriteLine("Tiempo invervalo: ;" + tiempo);
                    }
                    //clientsQuantity += clientsNew.Count();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error cliente: " + clientNew.Identification);
                    throw;
                }

            }

            public void MigraTarjetasOrden29()
            {
                int[] states = new int[] { 1, 2, 3, 5, 6, 11, 12, 13 };
                Cards cardNew = new Cards();
                List<Cards> cardsNew = new List<Cards>();

                //var clientesCreados = newContext.Clients.ToList(); //Clients.Select(a => new { ID = a.Id, a.Identification }).ToList();
                var branches = phiAdminContext.Branches.Where(branch => branch.InstitutionId == institutionID).ToList();
                var cardsO = context.TARJETA.ToArray();

                var oldClients = context.CLIENTE.ToList();
                var newClients = newContext.Clients.ToList();

                var clientesCreados = newClients
                    .Join(oldClients,
                    cc => cc.Identification,
                    c => c.CLI_IDENTIFICACION,
                    (cc, c) => new { cc, c })
                    .Select(a => new { ID = a.cc.Id, a.cc.Identification, CliCodigo = a.c.CLI_CODIGO })
                    .ToList();

                var cardsOld = clientesCreados
                    .Join(context.TARJETA_CUENTA,
                    c => c.CliCodigo,
                    tc => tc.CLI_CODIGO,
                    (c, tc) => new { c, tc })
                    .Join(context.TARJETA,
                    t => t.tc.TAR_ID,
                    tar => tar.TAR_ID,
                    (t, tar) => new { t, tar })
                    .Where(c => states.Contains(c.tar.EST_TAR_CODIGO))
                    .Select(tarjeta => new
                    {
                    Id = tarjeta.tar.TAR_ID,
                    OrderId = tarjeta.tar.ORD_CODIGO,
                    State = tarjeta.tar.EST_TAR_CODIGO,
                    ExpirationDate = tarjeta.tar.TAR_FECHA_EXPIRACION,
                    LastMovDate = tarjeta.tar.TAR_FECHA_ULTIMO_MOV,
                    EmissionDate = tarjeta.tar.TAR_FECHA_SOLICITUD,
                    PINLastChangeDate = tarjeta.tar.TAR_FECHA_ULT_CAMBIOPIN,
                    ClientID = tarjeta.t.c.ID,
                    ClientIdentification = tarjeta.t.c.Identification,
                    OFFSET = tarjeta.tar.TAR_OFFSET,
                    ATC_EMV = tarjeta.tar.TAR_EMV_SEC,
                    PrintedName = tarjeta.tar.TAR_NOMBRE_IMPRESO,
                    BranchId = tarjeta.tar.AGE_CODIGO,
                    MaskedNumber = tarjeta.tar.TAR_NUMERO,
                    HASH = tarjeta.tar.TAR_HASH,
                    //CardNumber = cards.card.tar2.tar1.tar.TAR_NUMERO_E,
                    CardNumber = tarjeta.tar.TAR_NUMERO,
                    CardDeliveryDate = tarjeta.tar.TAR_FECHA_ENTREGA,
                    CardAccount = tarjeta.t.tc.CTA_NUMERO,
                }).Take(100).ToArray();

                using (var file = File.CreateText(pathCards))
                {
                    foreach (var card in cardsOld)
                    {
                        int state = (card.State == 12 || card.State == 1) ? ((card.State == 12) ? 1 : 12) : card.State;
                        //int order = Convert.ToInt32(card.OrderId) >= 620 ? Convert.ToInt32(card.OrderId) : 0;
                        cardNew = new Cards()
                        {
                            Id = Convert.ToInt32(card.Id),
                            ProfileId = 1,
                            OrderId = card.OrderId,//order,
                            BatchId = 1,
                            State = state,
                            ExpirationDate = card.ExpirationDate,
                            LastMovDate = card.LastMovDate,
                            EmissionDate = card.EmissionDate,
                            PINLastChangeDate = card.PINLastChangeDate,
                            ClientId = card.ClientID,
                            CashQuota = 300,
                            PurchaseQuota = 0,
                            OFFSET = card.OFFSET,
                            Sequential = "",
                            PINAttempts = 0,
                            ATC_EMV = card.ATC_EMV,
                            PrintedName = card.PrintedName,
                            DeliveryBranchId = card.BranchId,
                            ModelId = 1,
                            MaskedNumber = card.MaskedNumber,
                            HASH = card.HASH,
                            Seed = "",
                            ChangePIN = false,
                            CardNumber = card.Id.ToString(),
                            LastCashDate = card.LastMovDate,
                            //LastPurchaseDate = card.LastMovDate,
                            CardDeliveryDate = card.CardDeliveryDate,
                            CardActivationDate = card.EmissionDate,
                            CashTransPerDay = 0,
                            RequestBranchId = branches.Where(branch => branch.CoreIdentifier == card.BranchId.ToString()).Select(branch => branch.Id).FirstOrDefault()
                        };

                        cardsNew.Add(cardNew);

                        file.WriteLine(card.CardNumber + ";" + card.ClientID + ";" + card.ClientIdentification + ";" + card.OrderId + ";" + card.State);
                    }
                }
                try
                {
                    newContext.Cards.AddRange(cardsNew);
                    cardsQuantity += cardsNew.Count();
                    cardsTotalQuantity += cardsQuantity;
                    newContext.SaveChanges();
                }
                catch (System.Exception)
                {
                    Console.WriteLine("error tarjeta cuenta: " + cardNew.CardNumber);
                    throw;
                }
            }

            public void MigraCuentasTarjeta29()
            {
                CardsAccounts account = new CardsAccounts();
                List<CardsAccounts> accounts = new List<CardsAccounts>();
                var tarjetasCreadas = newContext.Cards.Select(a => new { ID = a.Id, tarNumero = a.CardNumber }).ToList();
                var cuentas = context.TARJETA_CUENTA.ToList();

                var accountsM = tarjetasCreadas
                    .Join(cuentas,
                    t => Convert.ToInt32(t.tarNumero),
                    tc => tc.TAR_ID,
                    (t, tar) => new { t, tar })
                    .Select(tarjeta => new
                    {
                        CardId = tarjeta.t.ID,
                        AccountNumber = tarjeta.tar.CTA_NUMERO
                    }).Take(100).ToList();

                
                using (var file = File.CreateText(pathAccounts))
                {
                    foreach (var acc in accountsM)
                    {
                        account = new CardsAccounts
                        {
                            CardId = acc.CardId,
                            AccountNumber = acc.AccountNumber,
                            AccountType = "AHORROS",
                            IsPrincipal = true
                        };
                        accounts.Add(account);
                        file.WriteLine(account.CardId + ";" + account.AccountNumber);
                    }
                }
                try
                {
                    newContext.CardsAccounts.AddRange(accounts);
                    newContext.SaveChanges();
                    accountsQuantity += accounts.Count();
                }
                catch (System.Exception)
                {
                    Console.WriteLine("error cuenta: ");
                    throw;
                }
            }

            #endregion 29Oct
        }


        public static void writeGeneral(string message)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, message + "\n");
            }
            else
            {
                File.AppendAllText(path, message + "\n");
            }
        }
    }
}
