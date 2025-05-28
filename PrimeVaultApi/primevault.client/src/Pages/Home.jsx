import React, { useEffect, useState } from 'react';
import './Home.css';
import { Link } from 'react-router-dom';

function Home() {
    const [usuario, setUsuario] = useState(null);
    const [saldo, setSaldo] = useState(null);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);

    useEffect(() => {
        const carregarDados = async () => {
            try {
                const resUsuario = await fetch('http://localhost:5000/api/Usuario/1');
                if (!resUsuario.ok) throw new Error('Erro ao buscar usuário');
                const dadosUsuario = await resUsuario.json();
                setUsuario(dadosUsuario);

                let saldoAtual = dadosUsuario.saldo ?? null;

                if (saldoAtual === null) {
                    const resSaldo = await fetch(`http://localhost:5000/api/conta/saldo/usuario/${dadosUsuario.id}`);
                    if (!resSaldo.ok) throw new Error('Erro ao buscar saldo');
                    const dadosSaldo = await resSaldo.json();

                    if (typeof dadosSaldo === 'number') {
                        saldoAtual = dadosSaldo;
                    } else if (dadosSaldo?.saldo) {
                        saldoAtual = dadosSaldo.saldo;
                    } else if (Array.isArray(dadosSaldo) && dadosSaldo[0]?.saldo !== undefined) {
                        saldoAtual = dadosSaldo[0].saldo;
                    } else {
                        saldoAtual = 'Saldo não disponível';
                    }
                }

                setSaldo(saldoAtual);
            } catch (e) {
                setErro(e.message);
            } finally {
                setCarregando(false);
            }
        };

        carregarDados();
    }, []);

    if (carregando) return <p>Carregando...</p>;
    if (erro) return <p>Erro: {erro}</p>;
    if (!usuario) return <p>Usuário não encontrado.</p>;

    return (
        <div className='home-page'>
            <div className='home-card'>
                <section className='welcome-section'>
                    <h1 className='welcome-title'>Bem-vindo(a), {usuario.nome}</h1>
                    <p>Acompanhe suas informações abaixo.</p>
                </section>

                <section className='user-info-card'>
                    <h2 className='user-info-title'>Suas Informações</h2>
                    <div className='info-item'>
                        <span className='info-label'>Nome:</span>
                        <span className='info-value'>{usuario.nome}</span>
                    </div>
                    <div className='info-item'>
                        <span className='info-label'>Saldo Atual:</span>
                        <span className='info-value'>
                            {typeof saldo === 'number' ? `R$ ${saldo.toFixed(2)}` : saldo}
                        </span>
                    </div>
                </section>

                
                <div className="navigation-button">
                    <Link to="/ContaPage">
                        <button>Gerenciar Conta</button>
                    </Link>
                </div>
            </div>
        </div>
    );
}

export default Home;
