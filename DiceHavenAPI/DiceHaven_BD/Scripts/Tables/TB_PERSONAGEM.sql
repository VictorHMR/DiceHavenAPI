CREATE TABLE IF NOT EXISTS tb_personagem(
ID_PERSONAGEM INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
DS_NOME VARCHAR(75) NOT NULL,
DS_BACKSTORY TEXT,
DS_FOTO TEXT,
NR_IDADE INT NOT NULL,
DS_GENERO VARCHAR(20),
DS_CAMPO_LIVRE TEXT,
ID_USUARIO INT NOT NULL,
FOREIGN KEY(ID_USUARIO) REFERENCES TB_USUARIO(ID_USUARIO)
)ENGINE=InnoDB;
