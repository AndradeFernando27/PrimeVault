import { useState } from "react";

import { toast } from 'react-toastify';
import './LoginPage.css'
function LoginPage() {

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

    const [form, setForm] = useState({
        Login: '',
        Senha: '',
    })


    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });

    };

    const login = async (data) =>{

        console.log(data);
        const responce = await fetch("http://localhost:5000/api/usuario/login", {
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        const dataUsuario = await responce.json();
        console.log(dataUsuario);
        localStorage.setItem("usuarioId", dataUsuario.id);

        if(!responce.ok){
            showError("Erro ao fazer login");
            return
        }
        
        showSuccess("Login realizado com sucesso!");

        
    } 

    const handleSubmit = async (e) => {
        e.preventDefault();

        const UserLogin = {
            Login: form.Login,
            Senha: form.Senha
        };

        try {

            await login(UserLogin);

            
            window.location.href = '/Home';
            
        } catch (err) {
            console.log(err);
            showError('Erro ao fazer login');
        }

    };

    return (
        
            <div className="login-container">
                <h1 className="login-title">LoginPage</h1>

                <form className="login-form" onSubmit={handleSubmit}>
                    <div className="input-group">
                        <input
                            className="login-input"
                            type="text"
                            name="Login"
                            value={form.Login}
                            onChange={handleChange}
                            required
                            placeholder=" "
                        />
                        <label className="login-label">Login</label>
                    </div>

                    <div className="input-group">
                        <input
                            className="login-input"
                            type="password"
                            name="Senha"
                            value={form.Senha}
                            onChange={handleChange}
                            required
                            placeholder=" "
                        />
                        <label className="login-label">Senha</label>
                    </div>

                    
                    <button type="submit">Login</button>
                    <button type="reset">Cancelar</button>
                </form>
            </div>
       
    );

}

export default LoginPage;