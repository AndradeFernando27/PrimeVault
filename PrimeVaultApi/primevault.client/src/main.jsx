import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import App from './App.jsx';
import RegistrarConta from './components/RegistrarConta.jsx';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

createRoot(document.getElementById('root')).render(
  <StrictMode>
        <BrowserRouter>
            <AuthProvider>
                <Routes>
                    <Route path="/registrar-conta" element={<RegistrarConta/>} />
                </Routes>
                <ToastContainer />
            </AuthProvider>
        </BrowserRouter>
  </StrictMode>,
)
