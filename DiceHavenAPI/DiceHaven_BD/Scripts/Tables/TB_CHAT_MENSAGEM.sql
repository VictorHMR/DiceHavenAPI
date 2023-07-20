CREATE TABLE IF NOT EXISTS tb_chat_mensagem(
ID_CHAT_MENSAGEM INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
DS_MENSAGEM TEXT NOT NULL,
DT_DATA_ENVIO DATETIME NOT NULL,
FL_EDITADA BOOL NOT NULL,
DS_LINK_IMAGEM TEXT,
ID_USUARIO INT NOT NULL,
FL_ATIVA BOOL NOT NULL,
FL_VISUALIZADA BOOL NOT NULL,
ID_CHAT INT NOT NULL,
FOREIGN KEY(ID_USUARIO) REFERENCES tb_usuario(ID_USUARIO),
FOREIGN KEY(ID_CHAT) REFERENCES tb_chat(ID_CHAT)
)ENGINE=InnoDB;
