import React, { useEffect } from 'react';
import './ContaPage.css';
import { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import { cache } from 'react';


const VALOR_PADRAO_CONTA = {
    UserId: '',
    NumeroConta: '',
    TipoConta: '',
    Saldo: 0,
};

const VALOR_PADRAO_USUARIO = {
    Nome: '',
    Email: '',
    Senha: '',
};

const ContaPage = () => {
    const [contas, setContas] = useState([]);
    const [usuarios, setUsuarios] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [contaFormData, setContaFormData] = useState(VALOR_PADRAO_CONTA);
    const [usuarioFormData, setUsuarioFormData] = useState(VALOR_PADRAO_USUARIO);
    const [isEditing, setIsEditing] = useState(false);
    

    const getContas = async () => {

        try {
            const response = await fetch("http://localhost:5000/api/conta/todos", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });
            const data = await response.json();
            setContas(data);
        } catch (error) {
            showError(error);
        }
    };


    const getUsuarios = async () => {

        try {
            const response = await fetch("http://localhost:5000/api/usuario/todos", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });
            const data = await response.json();
            setUsuarios(data);
        } catch (error) {
            showError(error);
        }
    };


    useEffect(() => {
        getContas();
        getUsuarios();
    }, []);


    const procurarContas = async () => {
        try {

            const response = await fetch(`http://localhost:5000/api/conta/${searchQuery}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                throw new Error("Conta não encontrada");
            }

            const conta = await response.json();

            setContas([conta]);
        } catch (error) {
            showError(error.message || error);
        }
    };

    const editConta = (NumeroConta) => {
        

        const contaEditada = contas.find((conta) => conta.numeroConta === NumeroConta);

        const usuarioEditado = usuarios.find(u => u.id === contaEditada.user_id);

        
        if (contaEditada && usuarioEditado) {
            setContaFormData({
                
                NumeroConta: contaEditada.numeroConta,
                TipoConta: contaEditada.tipoConta,
            });
            
            setUsuarioFormData({
                Nome: usuarioEditado.nome,
                Email: usuarioEditado.email,
                Senha: usuarioEditado.senha,
            });
            setIsEditing(true);
        }
    };

    const handleContaChange = async (e) => {

        setContaFormData({ ...contaFormData, [e.target.name]: e.target.value });
 
    };

    const handleUsuarioChange = async (e) => {
        setUsuarioFormData({ ...usuarioFormData, [e.target.name]: e.target.value });
    }

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            procurarContas();
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (isEditing) {


            const data = {
                UsuarioEditDto: {
                    Nome: usuarioFormData.Nome,
                    Email: usuarioFormData.Email,
                    Senha: usuarioFormData.Senha,
                },
                ContaEditDto: {
                    NumeroConta: contaFormData.NumeroConta,
                    TipoConta: contaFormData.TipoConta,
                }
            }

            if (data.ContaEditDto.NumeroConta === '' ||
                data.ContaEditDto.TipoConta === '' ||
                data.UsuarioEditDto.Nome === '' ||
                data.UsuarioEditDto.Email === '' ||
                data.UsuarioEditDto.Senha === '') {

                showError('Por favor complete todos os campos');
                return;

            }

            const response = await fetch("http://localhost:5000/api/conta/atualizar-todos", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (!response.ok) {
                showError("Erro ao atualizar a conta!");
                
            }
            showSuccess('A conta foi atualizada com sucesso');

        } else {

            const data = {
                UsuarioCriarDto: {
                    Nome: usuarioFormData.Nome,
                    Email: usuarioFormData.Email,
                    Senha: usuarioFormData.Senha,
                },
                ContaDto: {
                    NumeroConta: contaFormData.NumeroConta,
                    TipoConta: contaFormData.TipoConta,
                    Saldo: 0,
                }
            }
            try {
            var response = await fetch("http://localhost:5000/api/conta/cadastrar-todos", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (!response) {
                showError("Erro ao cadastrar a conta!");
                return;
            }

            showSuccess("A conta foi cadastrada com sucesso!");
            }
            catch (error) {
                showError("Erro de rede ao cadastrar a conta!");
            }

            }
            setContaFormData({
                NumeroConta: '',
                TipoConta: '',
                Saldo: 0,
            });
            setUsuarioFormData({
                Nome: '',
                Email: '',
                Senha: '',
            });
            setIsEditing(false);
            getUsuarios();
            getContas();
        
    };

    const handleInputChange = (e) => {
        const value = e.target.value;
        setSearchQuery(value);
        if (value === "")
            getContas();
            getUsuarios();

    };


    const deleteUsuario = async (Email) => {
        try {
            await fetch(`http://localhost:5000/api/usuario/delete/${Email}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
            }
            )
            showSuccess('A conta foi deletada com sucesso');
            getContas();
        } catch (error) {
            showError(error);
        }
    }

    const showError = (message) => {
        toast.error(message, {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: false,
            draggable: true,
            progress: undefined,

        });
    };

    const showSuccess = (message) => {
        toast.success(message, {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: false,
            draggable: true,
            progress: undefined,
        });
    };

    

    return (
        <>

            <div className='conta-page'>
                <div className='conta-card'>
                    <section className='conta-form-section'>
                        <h1 className='conta-title'>Gerenciar Conta</h1>
                        <h3 className='conta-sub-title'>{isEditing ? 'Editar Conta' : 'Registrar Conta'}</h3>
                        <div className='conta-form'>
                            <div className='form-group'>
                                <input
                                    name="Nome"
                                    value={usuarioFormData.Nome}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleUsuarioChange}
                                />
                                <label className='form-label'>Nome</label>
                            </div>
                            <div className={isEditing ? 'none' : 'form-group' }>
                                <input
                                    name="Email"
                                    value={usuarioFormData.Email}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleUsuarioChange}
                                />
                                <label className='form-label'>Email</label>
                            </div>
                            <div className='form-group'>
                                <input
                                    name="Senha"
                                    type="password"
                                    value={usuarioFormData.Senha}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleUsuarioChange}
                                />
                                <label className='form-label'>Senha</label>
                            </div>                         
                            <div className='form-group'>
                                <input
                                    name="TipoConta"
                                    value={contaFormData.TipoConta}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleContaChange}
                                />
                                <label className='form-label'>Tipo da Conta</label>
                            </div>
                            <div className={isEditing ? 'none' : 'form-group'}>
                                <input
                                    name="NumeroConta"
                                    value={contaFormData.NumeroConta}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleContaChange}
                                />
                                <label className='form-label'>Numero da Conta</label>
                            </div>
                        </div>
                    </section>

                    <section className='conta-list-section'>
                        <div className='search-bar'>
                            <h2 className=' -title'>Contas Cadastradas</h2>
                            <div className='search-wrapper'>
                                <input
                                    type='text'
                                    name='search'
                                    className='search-input'
                                    placeholder="Procurar Conta"
                                    value={searchQuery}
                                    onChange={handleInputChange}
                                    onKeyDown={handleKeyDown}
                                />
                            </div>
                        </div>

                        <div className='conta-list'>
                            {contas.map((conta) => {
                                const usuario = usuarios.find((u) => u.Id === conta.UserId);
                                console.log('Ussuario:', usuario);
                                console.log('conta:', conta);

                                return (

                                    <div className='conta-item' key={conta.id}>
                                        <div className="conta-details">
                                            <div>Nome:</div>
                                            <span>{usuario?.nome ?? '—'}</span>
                                            <div>Email:</div>
                                            <span>{usuario?.email ?? '-'}</span>
                                            <div>Senha:</div>
                                            <span>*******</span>
                                            <div>Numero da conta:</div>
                                            <span>{conta.numeroConta}</span>
                                            <div>Tipo da conta:</div>
                                            <span>{conta.tipoConta}</span>
                                            <div>Saldo:</div>
                                            <span>{conta.saldo}</span>
                                        </div>
                                        <div className='conta-actions'>
                                            <button
                                                className='action-button'
                                                onClick={() => editConta(conta.numeroConta)}
                                            >
                                                <EditIcon />
                                            </button>
                                            <button
                                                className='action-button delete'
                                                onClick={() => deleteUsuario(usuario.email)}
                                            >
                                                <DeleteOutlineIcon />
                                            </button>
                                        </div>
                                    </div>)
                            })}
                        </div>
                    </section>

                    <section className='form-buttons'>
                        <button type="reset"
                            className='btn btn-secondary'
                            onClick={() => {
                                setContaFormData(VALOR_PADRAO_CONTA);
                                setUsuarioFormData(VALOR_PADRAO_USUARIO);
                                setIsEditing(false);
                                setSearchQuery('');
                                getContas(); }}>Cancelar</button>      
                        <button
                            type="button"
                            className='btn btn-primary'
                            onClick={handleSubmit}
                        >
                            {isEditing ? 'Salvar Alteracoes' : 'Registrar Conta'}</button>
                    </section>
                </div>
                <ToastContainer />
            </div>
        </>
    );
};

export default ContaPage;
