using GymApp.CORE.Entities;
using GymApp.CORE.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GymApp.Infrastructure.Implementation
{
    public class ClientServices : IClientServices
    {
        private readonly GymDbContext _dbContext;
        public ClientServices(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Client> GetClients()
        {
            List<Client> clients = _dbContext.Clients.ToList();
            return _dbContext.Clients.ToList();
        }

        public bool CreateClient(Client client)
        {
            try
            {
                _dbContext.Clients.Add(client);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void UpdateClient(Client client)
        {
            try
            {
                _dbContext.Attach(client);
                _dbContext.Entry(client).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool UpdateClientPayment(Client client)
        {
            try
            {
                Payment payment = new Payment();
                payment.Amount = 200;
                payment.Id = client.IdClient;
                payment.StartDate = DateTime.Now;
                payment.EndDate = DateTime.Now.AddMonths(1);
                payment.NextPaymentDate = DateTime.Now.AddMonths(1).AddDays(1);
                payment.IsOk = true;
                _dbContext.Payments.Add(payment);
                _dbContext.SaveChanges();
                payment = _dbContext.Payments.Where(p => p.Id == client.IdClient && p.StartDate.Day == DateTime.Now.Day).OrderByDescending(p => p.IdPayment).First();
                if(payment != null)
                {
                    client.IdPaymentOnGoing = payment.IdPayment;
                    _dbContext.SaveChanges();
                    UpdateClient(client);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public int GetIdPaymentOnGoing(int IdClient)
        {
            Client c = GetClientById(IdClient);
            if (c != null)
                return c.IdPaymentOnGoing;
            else
                return 0;
        }
        public Client GetClientById(int Id)
        {
            return _dbContext.Clients.Find(Id);
        }

        public void DeleteClient(int Id)
        {
            Client client = GetClientById(Id);
            var payments = _dbContext.Payments.Where(p => p.Id == Id);
            if(payments != null && payments.Count() > 0)
            {
                foreach(var p in payments)
                {
                    _dbContext.Payments.Remove(p);
                }
                _dbContext.SaveChanges();
            }
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                _dbContext.SaveChanges();
            }
        }

        public Payment GetPayement(int? idPayement)
        {
            var payement = (idPayement != null) ? _dbContext.Payments.Where(p => p.IdPayment == idPayement).FirstOrDefault() : null;
            return payement;
        }

        public bool isPaymentOK(int id)
        {
            bool IsPOK = false;
            Payment payment = GetPayement(id);
            if (payment != null)
            {
                if (DateTime.Now < payment.NextPaymentDate)
                {
                    IsPOK = true;
                }
            }
            return IsPOK;
        }
    }
}
