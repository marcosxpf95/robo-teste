import axios from "axios";
import { roboDto } from "./types/roboDto";
import { validacaoResponse } from "./types/validacaoResponse";

const api = axios.create({
  baseURL: "http://localhost:5231/api/",
  timeout: 10000,
  headers: { "Content-Type": "application/json" },
});

// Função para buscar movimentos
export const getRobo = async (): Promise<roboDto> => {
  const response = await api.get<roboDto>("robo");
  return response.data;
};

export const inclinarCabeca = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>("robo/cabeca/inclinar", {
    comando: comandoValor,
  });
  return response.data;
};

export const rotacionarCabeca = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>("robo/cabeca/rotacionar", {
    comando: comandoValor,
  });
  return response.data;
};

export const contrairBracoDireito = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>(
    "robo/braco/direito/contrair",
    { comando: comandoValor }
  );
  return response.data;
};

export const rotacionarBracoDireito = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>(
    "robo/braco/direito/rotacionar",
    { comando: comandoValor }
  );
  return response.data;
};

export const contrairBracoEsquerdo = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>(
    "robo/braco/esquerdo/contrair",
    { comando: comandoValor }
  );
  return response.data;
};

export const rotacionarBracoEsquerdo = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  const response = await api.put<validacaoResponse>(
    "robo/braco/esquerdo/rotacionar",
    { comando: comandoValor }
  );
  return response.data;
};

export default api;
