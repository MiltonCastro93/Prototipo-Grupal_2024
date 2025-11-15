# 🎮 Prototipo-Grupal_2024  
Prototipo inspirado en *Metal Gear Solid (PS1)*, creado para practicar sistemas de sigilo, detección y comportamiento básico de IA en Unity. Fue desarrollado como un ejercicio grupal de prototipado rápido, priorizando la lógica y las mecánicas por encima del arte o la calidad de producción.

---

## 🕹️ Descripción General
Este proyecto busca recrear los fundamentos del stealth clásico:

- IA con estados tipo MGS1  
- Cono de visión de enemigos  
- Detección visual y sonora  
- Mecánicas básicas de sigilo  
- Reacciones del enemigo ante ruido y movimiento  

El prototipo se centra puramente en **gameplay y lógica**, sin pulido visual ni sonoro.

---

## 🖼️ Capturas

<p align="left">
  <img src="https://github.com/MiltonCastro93/Prototipo-Grupal_2024/blob/main/Captura%20de%20pantalla%202025-11-15%20143214.png" width="400"/>
</p>
<p align="center">
  <img src="https://github.com/MiltonCastro93/Prototipo-Grupal_2024/blob/main/Captura%20de%20pantalla%202025-11-15%20143305.png" width="400"/>
</p>
<p align="right">
  <img src="https://github.com/MiltonCastro93/Prototipo-Grupal_2024/blob/main/Captura%20de%20pantalla%202025-11-15%20143342.png" width="400"/>
</p>

---

## 👤 Jugador
El jugador cuenta con habilidades simples, suficientes para probar el sistema de detección:

- Caminar y correr  
- Agacharse para reducir visibilidad  
- Evitar conos de visión  
- Evitar producir ruido al moverse  

Estas mecánicas interactúan directamente con los sistemas de IA para generar situaciones típicas de sigilo.

---

## 👁️‍🗨️ IA de los Enemigos  
Los enemigos poseen una lógica inspirada en los guardias clásicos de Metal Gear Solid.  
Los estados implementados son:

### 🔸 **Patrol (Patrulla)**
Aunque el prototipo no utiliza NavMesh (los enemigos no caminan), el estado está implementado a nivel lógico para futuras expansiones.

### 🔸 **Detection (Detección)**
El enemigo detecta visualmente al jugador si entra en su cono de visión.

### 🔸 **Hearing (Oído / Sospecha)**
Se implementaron dos sistemas de sonido:

- **Esfera de eventos auditivos:**  
  Si el jugador genera ruido dentro de esta área, el enemigo entra en estado de sospecha.

- **Charcos:**  
  Si el jugador pisa un charco, el sonido se transmite a los enemigos cercanos.

Los enemigos reaccionan con cambios de estado aunque no posean animaciones ni desplazamiento.

---

## 🔧 Implementación Técnica
Todo el comportamiento de IA fue desarrollado **desde cero** con scripts personalizados:

- Sistema de visión (ángulo, distancia y obstrucción)  
- Sistema de audición basado en colisiones y triggers  
- Máquina de estados con Patrol / Search / Alert  
- Lógica modular pensada para escalar a una IA completa  
- Eventos de juego centralizados en un único script principal  

Esto permitió mantener el prototipo ágil y fácil de expandir.

---

## 🎨 Arte y Audio
- **Arte:** descargado desde Google con fines únicamente educativos y temporales.  
- **Audio:** no posee.  
El enfoque fue completamente funcional, no artístico.

---

## 🚧 Estado del Proyecto
Este es un prototipo funcional **incompleto**, pero ideal para evolucionar a:

- IA con movimiento real usando NavMesh  
- Animaciones de patrulla  
- Sistema de alerta completo  
- Sonidos reales de pasos, charcos, alarmas  
- Mejoras visuales del escenario  

---

## 📬 Contacto
Si querés hacer consultas o colaborar con mejoras, mis redes sociales estan en el readme principal!
<li>📫 Contacto: <a href="https://github.com/MiltonCastro93"><b>Clic Aqui</b></a></li>

---

## 📄 Licencia
Este prototipo se comparte con fines educativos, experimentales y de estudio.
