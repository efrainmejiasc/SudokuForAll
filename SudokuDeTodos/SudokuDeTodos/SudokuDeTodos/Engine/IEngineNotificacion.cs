﻿using SudokuDeTodos.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuDeTodos.Engine
{
    public interface IEngineNotificacion
    {
        bool EnviarMailNotificacion(EstructuraMail model);
    }
}
