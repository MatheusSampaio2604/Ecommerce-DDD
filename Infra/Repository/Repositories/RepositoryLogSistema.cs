﻿using Domain.Interfaces.InterfaceLogSistema;
using Entities.Entities;
using Infrastructure.Repository.Generics;

namespace Infra.Repository.Repositories
{
    public class RepositoryLogSistema : RepositoryGenerics<LogSistema>, ILogSistema
    {
    }
}
