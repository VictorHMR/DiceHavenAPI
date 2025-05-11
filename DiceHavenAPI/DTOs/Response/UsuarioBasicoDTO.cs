namespace DiceHaven_API.DTOs.Response
{
    public class UsuarioBasicoDTO
    {
        public int ID_USUARIO {  get; set; }
        public string DS_NOME { get; set; }
        public string DS_FOTO { get; set; }
        public bool FL_ADMIN { get; set; }
    }
}
