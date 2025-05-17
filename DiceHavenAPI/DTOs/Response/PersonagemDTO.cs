namespace DiceHaven_API.DTOs.Response
{

    public class PersonagemDTO
    {
        public int? ID_PERSONAGEM { get; set; }
        public string DS_NOME { get; set; }
        public string DS_FOTO { get; set; }
        public string DS_COR { get; set; }
        public int ID_USUARIO { get; set; }
    }
}