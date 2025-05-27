import React, { useEffect } from 'react';
import './ContaPage.css';
import { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';


const DEFAULT_FORM_VALUES = {
    UserId: '',
    NumeroConta: '',
    TipoConta: '',
    Saldo: 0,
};

const ContaPage = () => {
    const [contas, setContas] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [formData, setFormData] = useState(DEFAULT_FORM_VALUES);
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


    useEffect(() => {
        getContas();
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
        const contaEditada = contas.find((conta) => conta.NumeroConta === NumeroConta);
        if (contaEditada) {
            setFormData({
                UserId: contaEditada.UserId,
                NumeroConta: contaEditada.NumeroConta,
                TipoConta: contaEditada.TipoConta,
            });
            setIsEditing(true);
        }
    };

    const handleChange = async (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });

    };

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            procurarContas();
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        
        
        if (isEditing) {

            const contaData = {
                UserId: formData.UserId,
                NumeroConta: formData.NumeroConta,
                TipoConta: formData.TipoConta,
            }
            if (contaData.UserId === '' ||
                contaData.NumeroConta === '' ||
                contaData.TipoConta === '') {
                showError('Por favor complete todos os campos');
                return;
            }
                await fetch("http://localhost:5000/api/conta/atualizar", {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(contaData),
                });
                showSuccess('A conta foi atualizada com sucesso!');
        } else {

            const contaData = {
                UserId: formData.UserId,
                NumeroConta: formData.NumeroConta,
                TipoConta: formData.TipoConta,
                Saldo: 0,
            }
            if (contaData.UserId === '' ||
                contaData.NumeroConta === '' ||
                contaData.TipoConta === '') {
                showError('Por favor complete todos os campos');
                return;
            }

            console.log(contaData);

                await fetch("http://localhost:5000/api/conta/cadastrar", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(contaData),
                });
                showSuccess('A conta foi cadastrada com sucesso');
            }
            setFormData({
                UserId: '',
                NumeroConta: '',
                TipoConta: '',
                Saldo: 0,
            });
            setIsEditing(false);
            
            getContas();
        
        
    };

    const handleInputChange = (e) => {
        const value = e.target.value;
        setSearchQuery(value);
        if (value === "")
            getContas();

    };


    const deleteConta = async (NumeroConta) => {
        try {
            await fetch(`http://localhost:5000/api/conta/delete/${NumeroConta}`, {
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
                        <h1 className='conta-title'>{isEditing ? 'Editar Conta' : 'Registrar Conta'}</h1>
                        <div className='conta-form'>
                            <div className={isEditing ? 'none' : 'form-group'}>
                                <input
                                    name="UserId"
                                    value={formData.UserId}
                                    placeholder=" "
                                    className={isEditing ? 'none' : 'form-input'}
                                    onChange={handleChange}
                                />
                                <label className={isEditing ? 'none' : 'form-label'}>User Id</label>
                            </div>
                            <div className='form-group'>
                                <input
                                    name="NumeroConta"
                                    value={formData.NumeroConta}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleChange}
                                />
                                <label className='form-label'>Numero da Conta</label>
                            </div>
                            <div className='form-group'>
                                <input
                                    name="TipoConta"
                                    value={formData.TipoConta}
                                    placeholder=" "
                                    className='form-input'
                                    onChange={handleChange}
                                />
                                <label className='form-label'>Tipo da Conta</label>
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
                            {contas.map((conta) => (
                                <div className='conta-item' key={conta.id}>
                                    <div className="conta-details">
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
                                            onClick={() => editConta(conta.NumeroConta)}
                                        >
                                            <EditIcon />
                                        </button>
                                        <button
                                            className='action-button delete'
                                            onClick={() => deleteConta(conta.NumeroConta)}
                                        >
                                            <DeleteOutlineIcon />
                                        </button>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </section>

                    <section className='form-buttons'>
                        <button type="reset"
                            className='btn btn-secondary'
                            onClick={() => {
                            setFormData(DEFAULT_FORM_VALUES);
                                setIsEditing(false);
                                setSearchQuery('');
                                getContas(); }}>Cancel</button>      
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
