import React, { useEffect, useState } from "react";
import { Modal, Button, Typography, Box } from "@mui/material";
import { roboDto } from "../services/types/roboDto";
import {
  getRobo,
  rotacionarCabeca,
  inclinarCabeca,
  contrairBracoDireito,
  rotacionarBracoDireito,
  contrairBracoEsquerdo,
  rotacionarBracoEsquerdo,
} from "../services/api";
import Parte from "./Parte";

const Robo = () => {
  const [roboData, setRoboData] = useState<roboDto | null>(null);
  const [open, setOpen] = useState(false);
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
      setRoboData(data);
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarRotacionarCabeca = async (comando: string) => {
    try {
      await rotacionarCabeca(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarInclinarCabeca = async (comando: string) => {
    try {
      await inclinarCabeca(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarContrairBracoDireito = async (comando: string) => {
    try {
      await contrairBracoDireito(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarRotacionarBracoDireito = async (comando: string) => {
    try {
      await rotacionarBracoDireito(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarContrairBracoEsquerdo = async (comando: string) => {
    try {
      await contrairBracoEsquerdo(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const atualizarRotacionarBracoEsquerdo = async (comando: string) => {
    try {
      await rotacionarBracoEsquerdo(comando);
      atualizarEstado();
    } catch (error) {
      exibirError(error);
    }
  };

  const exibirError = (error: any) => {
    if (error instanceof Error) {
      if ((error as any).response) {
        const errorResponse = (error as any).response.data.message;
        setErrorMessage(`${errorResponse}`);
      } else {
        setErrorMessage(`Erro desconhecido: ${error.message}`);
      }
    } else {
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
            Nome="Cabeça Rotação"
            Estado={roboData.cabecaRotacao}
            ProximosEstados={roboData.proximosCabecaRotacao}
            onClick={atualizarRotacionarCabeca}
          />
          <Parte
            Nome="Cabeça Inclinação"
            Estado={roboData.cabecaInclinacao}
            ProximosEstados={roboData.proximosCabecaInclinacao}
            onClick={atualizarInclinarCabeca}
          />
          <Parte
            Nome="Braço Direito Cotovelo Contração"
            Estado={roboData.bracoDireitoCotoveloContracao}
            ProximosEstados={roboData.proximosBracoDireitoCotoveloContracao}
            onClick={atualizarContrairBracoDireito}
          />
          <Parte
            Nome="Braço Direito Pulso Rotação"
            Estado={roboData.bracoDireitoPulsoRotacao}
            ProximosEstados={roboData.proximosBracoDireitoPulsoRotacao}
            onClick={atualizarRotacionarBracoDireito}
          />
          <Parte
            Nome="Braço Esquerdo Cotovelo Contração"
            Estado={roboData.bracoEsquerdoCotoveloContracao}
            ProximosEstados={roboData.proximosBracoEsquerdoCotoveloContracao}
            onClick={atualizarContrairBracoEsquerdo}
          />
          <Parte
            Nome="Braço Esquerdo Pulso Rotação"
            Estado={roboData.bracoEsquerdoPulsoRotacao}
            ProximosEstados={roboData.proximosBracoEsquerdoPulsoRotacao}
            onClick={atualizarRotacionarBracoEsquerdo}
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
        sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center' }}
      >
        <Box sx={{ width: 400, padding: 2, backgroundColor: 'background.paper', borderRadius: 2 }}>
          <Typography variant="h6" id="error-modal-title">
            Movimento não permitido
          </Typography>
          <Typography id="error-modal-description" sx={{ mt: 2 }}>
            {errorMessage}
          </Typography>
          <Button onClick={handleClose} variant="contained" color="primary" sx={{ mt: 2 }}>
            Fechar
          </Button>
        </Box>
      </Modal>
    </div>
  );
};

export default Robo;
