import React, { useEffect, useState } from "react";
import { Modal, Button, Typography, Box } from "@mui/material";
import { roboDto } from "../services/types/roboDto";
import { getRobo, updateRobo } from "../services/api";
import Parte from "./Parte";

const Robo = () => {
  const [roboData, setRoboData] = useState<roboDto | null>(null);
  const [open, setOpen] = useState(false);
  const [titleErrorMessage, setTitleErrorMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getRobo();
        setRoboData(data);
      } catch (error) {
        exibirError(error);
      }
    };

    fetchData();
  }, []);

  const atualizarEstado = async () => {
    try {
      const data = await getRobo();
      console.log(data);
      setRoboData(data);
    } catch (error) {
      exibirError(error);
    }
  };

  const enviarComando = async (comando: string) => {
    try {
      var result = await updateRobo(comando);
      console.log(result);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const exibirError = (error: any) => {
    if (error instanceof Error) {
      if ((error as any).response) {
        const errorResponse = (error as any).response.data.message;
        setTitleErrorMessage("Movimento não permitido");
        setErrorMessage(`${errorResponse}`);
      } else {
        setTitleErrorMessage("Erro!");
        setErrorMessage(`Erro desconhecido: ${error.message}`);
      }
    } else {
      setTitleErrorMessage("Erro!");
      setErrorMessage(`Erro desconhecido: ${error}`);
    }
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      {roboData ? (
        <>
          <Parte
            nome="Cabeça Rotação"
            estado={roboData.cabecaRotacao}
            proximosEstados={roboData.proximosCabecaRotacao}
            comando="Cabeca:Rotacao"
            onClick={enviarComando}
          />
          <Parte
            nome="Cabeça Inclinação"
            estado={roboData.cabecaInclinacao}
            proximosEstados={roboData.proximosCabecaInclinacao}
            comando="Cabeca:Inclinacao"
            onClick={enviarComando}
          />
          <Parte
            nome="Braço Direito Cotovelo Contração"
            estado={roboData.bracoDireitoCotoveloContracao}
            proximosEstados={roboData.proximosBracoDireitoCotoveloContracao}
            comando="BracoDireito:Cotovelo"
            onClick={enviarComando}
          />
          <Parte
            nome="Braço Direito Pulso Rotação"
            estado={roboData.bracoDireitoPulsoRotacao}
            proximosEstados={roboData.proximosBracoDireitoPulsoRotacao}
            comando="BracoDireito:Pulso"
            onClick={enviarComando}
          />
          <Parte
            nome="Braço Esquerdo Cotovelo Contração"
            estado={roboData.bracoEsquerdoCotoveloContracao}
            proximosEstados={roboData.proximosBracoEsquerdoCotoveloContracao}
            comando="BracoEsquerdo:Cotovelo"
            onClick={enviarComando}
          />
          <Parte
            nome="Braço Esquerdo Pulso Rotação"
            estado={roboData.bracoEsquerdoPulsoRotacao}
            proximosEstados={roboData.proximosBracoEsquerdoPulsoRotacao}
            comando="BracoEsquerdo:Pulso"
            onClick={enviarComando}
          />
        </>
      ) : (
        <p>Carregando dados...</p>
      )}

      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="error-modal-title"
        aria-describedby="error-modal-description"
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <Box
          sx={{
            width: 400,
            padding: 2,
            backgroundColor: "background.paper",
            borderRadius: 2,
          }}
        >
          <Typography variant="h6" id="error-modal-title">
            {titleErrorMessage}
          </Typography>
          <Typography id="error-modal-description" sx={{ mt: 2 }}>
            {errorMessage}
          </Typography>
          <Button
            onClick={handleClose}
            variant="contained"
            color="primary"
            sx={{ mt: 2 }}
          >
            Fechar
          </Button>
        </Box>
      </Modal>
    </div>
  );
};

export default Robo;
