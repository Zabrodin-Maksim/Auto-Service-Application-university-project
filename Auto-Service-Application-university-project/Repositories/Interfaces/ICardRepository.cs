﻿using Auto_Service_Application_university_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories.Interfaces
{
    //TODO: DELETE
    public interface ICardRepository
    {
        IEnumerable<Card> GetAll();
        Card GetById(int id);
        void Add(Card card);
        void Update(Card card);
        void Delete(int id);
    }
}
