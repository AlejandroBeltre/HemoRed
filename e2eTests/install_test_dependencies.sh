#!/bin/bash

# Crear entorno virtual
python -m venv e2e_venv

# Activar entorno virtual
source e2e_venv/bin/activate   # Linux/Mac
# .\e2e_venv\Scripts\activate  # Windows

# Instalar dependencias
pip install -r e2eTests/requirements.txt

echo "Entorno de pruebas E2E configurado correctamente."