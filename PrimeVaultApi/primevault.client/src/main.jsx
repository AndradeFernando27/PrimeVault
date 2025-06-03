import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'; 

import LoginPage from './Pages/LoginPage/LoginPage.jsx';
import ContaPage from './Components/ContaPage.jsx';
import Home from "./Pages/Home.jsx";

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/Login" element={<LoginPage />}/>
                <Route path="/" element={<ContaPage />} />
                <Route path="/Home" element={<Home />} />
            </Routes>
            <ToastContainer /> 
        </BrowserRouter>
    </StrictMode>
);