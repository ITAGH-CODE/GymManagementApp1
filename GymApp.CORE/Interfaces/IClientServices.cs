using GymApp.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymApp.CORE.Interfaces
{
    public interface IClientServices
    {
        List<Client> GetClients();
        bool CreateClient(Client client);
        void UpdateClient(Client client);
        Client GetClientById(int Id);
        void DeleteClient(int Id);
        Payment GetPayement(int? idPayement);
        bool UpdateClientPayment(Client client);
        int GetIdPaymentOnGoing(int IdClient);
        bool isPaymentOK(int id);
    }
}
