using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_campo_ficha
{
    public int ID_CAMPO_FICHA { get; set; }

    public string DS_NOME_CAMPO { get; set; }

    public int NR_TIPO_CAMPO { get; set; }

    public string DS_REFERENCIA { get; set; }

    public string DS_DESCRICAO { get; set; }

    public bool FL_TEM_MODIFICADOR { get; set; }

    public bool? FL_BLOQUEADO { get; set; }

    public bool? FL_VISIVEL { get; set; }

    public string DS_FORMULA_MODIFICADOR { get; set; }

    public string DS_VALOR_PADRAO { get; set; }

    public int NR_ORDEM { get; set; }

    public bool FL_ATIVO { get; set; }

    public int ID_CAMPANHA { get; set; }

    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }

    public virtual ICollection<tb_dados_ficha> tb_dados_fichas { get; set; } = new List<tb_dados_ficha>();
}
