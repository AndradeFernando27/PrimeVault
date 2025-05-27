import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import ContaPage from './Components/ContaPage.jsx';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';  

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<ContaPage />} />
            </Routes>
            <ToastContainer /> 
        </BrowserRouter>
    </StrictMode>
);