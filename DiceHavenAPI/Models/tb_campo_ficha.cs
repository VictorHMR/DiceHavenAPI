using DiceHaven_API.Models;
using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_campo_ficha
{
    public int ID_CAMPO_FICHA { get; set; }

    public string DS_NOME_CAMPO { get; set; }

    public int NR_TIPO_CAMPO { get; set; }

    public bool? FL_BLOQUEADO { get; set; }

    public bool? FL_VISIVEL { get; set; }

    public bool FL_MODIFICADOR { get; set; }

    public string DS_VALOR_PADRAO { get; set; }

    public int NR_ORDEM { get; set; }

    public int ID_SECAO_FICHA { get; set; }

    public virtual tb_secao_ficha ID_SECAO_FICHANavigation { get; set; }

    public virtual ICollection<tb_dados_ficha> tb_dados_fichas { get; set; } = new List<tb_dados_ficha>();
}
