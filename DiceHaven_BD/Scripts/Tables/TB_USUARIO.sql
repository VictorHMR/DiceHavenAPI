CREATE TABLE IF NOT EXISTS tb_usuario(
  ID_USUARIO INT NOT NULL AUTO_INCREMENT,
  DS_NOME VARCHAR(100) NOT NULL,
  DT_NASCIMENTO DATETIME NOT NULL,
  DS_LOGIN VARCHAR(30) NOT NULL,
  DS_SENHA TEXT NOT NULL,
  DS_EMAIL  VARCHAR(100) NOT NULL,
  FL_ATIVO BOOL NOT NULL DEFAULT '0',
  DT_ULTIMO_ACESSO datetime DEFAULT NULL,
  PRIMARY KEY (ID_USUARIO)
) ENGINE=InnoDB;



