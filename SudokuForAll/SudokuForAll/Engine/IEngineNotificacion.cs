﻿using SudokuForAll.Models.DbSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForAll.Engine
{
    public interface IEngineNotificacion
    {
        bool EnviarMailNotificacion(EstructuraMail model);
    }
}
