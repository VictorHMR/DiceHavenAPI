CREATE TABLE IF NOT EXISTS tb_usuario_contato(
ID_USUARIO_CONTATO INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
ID_USUARIO INT NOT NULL,
ID_CONTATO INT NOT NULL,
FL_MUTADO BOOL NOT NULL,
FOREIGN KEY(ID_USUARIO) REFERENCES tb_usuario(ID_USUARIO),
FOREIGN KEY(ID_CONTATO) REFERENCES tb_usuario(ID_USUARIO)
)ENGINE=InnoDB;



