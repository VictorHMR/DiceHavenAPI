CREATE TABLE IF NOT EXISTS tb_raca(
ID_RACA INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
DS_RACA VARCHAR(30) NOT NULL,
DS_DESCRICAO TEXT,
DS_FOTO LONGBLOB,
NR_STR_PADRAO INT,
NR_DEX_PADRAO INT,
NR_CON_PADRAO INT,
NR_INT_PADRAO INT,
NR_WIS_PADRAO INT,
NR_CHA_PADRAO INT,
ID_CAMPANHA INT NOT NULL,
FOREIGN KEY(ID_CAMPANHA) REFERENCES tb_campanha(ID_CAMPANHA)
)ENGINE=InnoDB;