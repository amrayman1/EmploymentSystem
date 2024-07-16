﻿using EmploymentSystem.Core.Entities;
using EmploymentSystem.Core.Interfaces;
using EmploymentSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<ApplicationDetails>, IApplicationRepository
    {
        public ApplicationRepository(EmploymentDbContext context) : base(context) { }
    }
}

