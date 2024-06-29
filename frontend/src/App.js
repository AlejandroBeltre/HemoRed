import './App.css';
import React, {useState, useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import HomePage from './homePage/homePage';
import RegisterUser from './registerUser/registerUser';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<HomePage />}/>
        <Route path="/registerUser" element={<RegisterUser />}/>
      </Routes>
    </div>
  );
}

export default App; 
