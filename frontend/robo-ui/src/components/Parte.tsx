import React from "react";

interface ParteProps {
  Nome?: string;
  Estado?: string;
  ProximosEstados?: string[];
  onClick: (estado: string) => void;
}

function formatarTexto(input: string): string {
  const resultado = input
    .replace(/([a-z])([A-Z])/g, "$1 $2") // Adiciona espaço antes de letras maiúsculas após letras minúsculas
    .replace(/([A-Z])(\d)/g, "$1 $2") // Adiciona espaço entre letras maiúsculas e números
    .replace(/(\d)([A-Z])/g, "$1 $2") // Adiciona espaço entre números e letras maiúsculas
    .replace(/(\d)(\d)/g, " $1$2") // Adiciona espaço entre números consecutivos
    .trim();
  return resultado;
}

const Parte = ({ Nome, Estado, ProximosEstados, onClick }: ParteProps) => {
  return (
    <div className="parte-container">
      <div className="parte-header">
        <h1>{Nome}</h1>
        <p>Estado Atual: {formatarTexto(Estado!) || "N/A"}</p>
      </div>
      <div className="parte-buttons-container">
        <div className="parte-buttons-header">
          <p>Próximos Estados</p>
        </div>
        <div className="parte-buttons">
          {ProximosEstados &&
            ProximosEstados.map((estado, index) => (
              <button key={index} onClick={() => onClick(estado)}>
                {formatarTexto(estado)}
              </button>
            ))}
        </div>
      </div>
    </div>
  );
};

export default Parte;
