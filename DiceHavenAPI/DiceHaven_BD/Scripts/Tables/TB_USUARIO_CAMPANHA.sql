CREATE TABLE IF NOT EXISTS tb_usuario_campanha(
ID_USUARIO_CAMPANHA INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
ID_CAMPANHA INT NOT NULL,
ID_USUARIO INT NOT NULL,
FL_ADMIN BOOL NOT NULL,
FL_MUTADO BOOL NOT NULL,
DT_ENTRADA DATETIME NOT NULL,
FOREIGN KEY(ID_CAMPANHA) REFERENCES tb_campanha(ID_CAMPANHA),
FOREIGN KEY(ID_USUARIO) REFERENCES tb_usuario(ID_USUARIO)
)ENGINE=InnoDB;
