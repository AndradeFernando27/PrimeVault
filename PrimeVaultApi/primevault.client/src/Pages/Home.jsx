import React, { useEffect, useState } from "react";

function Home() {
  const [usuario, setUsuario] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5000/api/usuarios/1')
 // ajuste o endpoint se necessário
      .then((res) => {
        if (!res.ok) {
          throw new Error("Erro ao buscar usuário");
        }
        return res.json();
      })
      .then((data) => {
        setUsuario(data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Carregando usuário...</p>;
  if (error) return <p>Erro: {error}</p>;

  return (
    <div>
      <h1>Bem-vindo(a), {usuario.nome}!</h1>
      <p>Se você está vendo isso, o React está renderizando corretamente.</p>
    </div>
  );
}

export default Home;
