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

export const updateRobo = async (
  comandoValor: string
): Promise<validacaoResponse> => {
  console.log(comandoValor);
  const response = await api.put<validacaoResponse>("robo", {
    comando: comandoValor,
  });
  return response.data;
};

export default api;
