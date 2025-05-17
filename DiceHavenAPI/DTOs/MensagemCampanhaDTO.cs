namespace DiceHaven_API.DTOs
{
    public class MensagemCampanhaDTO
    {
        public int? ID_CAMPANHA_MENSAGEM { get; set; }
        public string DS_MENSAGEM { get; set; }
        public bool FL_MESTRE { get; set; }
        public DateTime? DT_MENSAGEM { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_CAMPANHA { get; set; }
        public int? ID_PERSONAGEM { get; set; }
    }
}
