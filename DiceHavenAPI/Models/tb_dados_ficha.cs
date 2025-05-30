﻿using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_dados_ficha
{
    public int ID_DADO_FICHA { get; set; }

    public int ID_CAMPO_FICHA { get; set; }

    public int ID_PERSONAGEM { get; set; }

    public string DS_VALOR { get; set; }

    public int? DS_VALOR_MODIFICADOR { get; set; }

    public virtual tb_campo_ficha ID_CAMPO_FICHANavigation { get; set; }

    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }
}
