using Domain.Interfaces.InterfaceLogSistema;
using Entities.Entities;
using Infrastructure.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class RepositoryLogSistema :  RepositoryGenerics<LogSistema>, ILogSistema
    {
    }
}
