# Mundo del Wumpus - Versión Gráfica con Windows Forms

Este proyecto es una adaptación del clásico juego "Mundo del Wumpus" a una interfaz gráfica de usuario (GUI) utilizando Windows Forms en C#. El objetivo principal es encontrar el oro dentro de un laberinto evitando caer en pozos y ser devorado por el temible Wumpus.

## Descripción del Juego

El jugador explora un laberinto compuesto por habitaciones interconectadas. Algunas habitaciones pueden contener peligros (pozos o el Wumpus) o el tesoro (oro). El jugador recibe pistas sensoriales en las casillas adyacentes a estos elementos:

*   **Hedor:** Indica que el Wumpus está en una casilla adyacente.
*   **Brisa:** Indica que hay un pozo en una casilla adyacente.

El jugador gana si encuentra el oro y pierde si cae en un pozo o entra en la misma casilla que el Wumpus.

## Características

*   **Interfaz Gráfica:** El juego se presenta en una ventana de Windows Forms, mostrando el laberinto de forma visual.
*   **Generación Aleatoria del Laberinto:**
    *   El tamaño del laberinto se genera aleatoriamente (entre 5x5 y 8x8 casillas).
    *   La posición inicial del jugador, el Wumpus, el oro y los pozos (3 por defecto) se colocan aleatoriamente en casillas únicas.
*   **Movimiento del Jugador:** El jugador se mueve utilizando las teclas W (arriba), A (izquierda), S (abajo), D (derecha).
*   **Indicadores Visuales:**
    *   Casillas no exploradas se muestran con un punto (`.`).
    *   Casillas visitadas se marcan con una `x`.
    *   La posición del jugador se indica con una `J`.
    *   Al finalizar el juego (victoria o derrota), se revelan las posiciones del Wumpus (`W`), Oro (`O`) y Pozos (`P`). (cambio: uso de Emoji)
*   **Pistas Sensoriales:** Se muestra un mensaje de estado indicando si el jugador siente "Hedor" o "Brisa" en su ubicación actual.
*   **Condiciones de Fin de Juego:**
    *   **Victoria:** Al encontrar el oro.
    *   **Derrota:** Al caer en un pozo o ser capturado por el Wumpus.
*   **Botón "Nuevo Juego":** Permite reiniciar la partida con un nuevo laberinto.

## Estructura del Proyecto

El proyecto está organizado de la siguiente manera dentro de la solución de Visual Studio:

*   **Proyecto `WumpusWinForms` (Aplicación Principal):**
    *   `Form1.cs`: Contiene la lógica de la interfaz de usuario, manejo de eventos (teclado, clics de botón) y la renderización del tablero de juego. Interactúa con las clases de lógica.
    *   `Form1.Designer.cs`: Código autogenerado por el diseñador de Windows Forms para los componentes visuales.
    *   `Program.cs`: Punto de entrada estándar para una aplicación Windows Forms, inicia `Form1`.
    *   **Carpeta `WumpusLogic`:** Contiene las clases que definen la lógica central y las reglas del juego.
        *   `Juego.cs`: Orquesta la partida, procesa movimientos y actualiza el estado del juego.
        *   `Laberinto.cs`: Representa el tablero, gestiona la colocación de elementos (Wumpus, oro, pozos) y la detección de pistas.
        *   `Jugador.cs`: Representa al jugador, su posición y movimiento.
        *   `ContenidoCasilla.cs`: Enumeración para el contenido de cada casilla (Vacío, Wumpus, Oro, Pozo, Visitado).
        *   `EstadoJuego.cs`: Enumeración para el estado actual del juego (Jugando, Ganado, PerdidoPorWumpus, PerdidoPorPozo).

## Cómo Compilar y Ejecutar

1.  **Requisitos:**
    *   Microsoft Visual Studio (versión 2019 o posterior recomendada, compatible con .NET Framework o .NET Core/5+ para Windows Forms).
    *   .NET SDK correspondiente al tipo de proyecto.
2.  **Abrir el Proyecto:**
    *   Clonar o descargar este repositorio.
    *   Abrir el archivo de solución (`.sln`) con Visual Studio.
3.  **Compilar:**
    *   En Visual Studio, seleccionar "Build" (Compilar) > "Build Solution" (Compilar Solución) o presionar `Ctrl+Shift+B`.
4.  **Ejecutar:**
    *   Presionar `F5` o hacer clic en el botón "Start" (Iniciar) en Visual Studio.

## Controles del Juego

*   **W / Flecha Arriba:** Mover hacia arriba.
*   **A / Flecha Izquierda:** Mover hacia la izquierda.
*   **S / Flecha Abajo:** Mover hacia abajo.
*   **D / Flecha Derecha:** Mover hacia la derecha.
*   **Botón "New Game":** Inicia una nueva partida.

## Proceso de Desarrollo: De Consola a GUI

El desarrollo de esta versión gráfica partió de una base de lógica de juego implementada previamente para una aplicación de consola. Los pasos principales de la transición fueron:

1.  **Creación del Proyecto Windows Forms:** Se estableció un nuevo proyecto de tipo "Aplicación de Windows Forms".
2.  **Integración de la Lógica Existente:** Las clases C# que definían las reglas del juego (`Juego.cs`, `Laberinto.cs`, etc.) se incorporaron directamente al proyecto de Windows Forms, agrupándolas en una carpeta (`WumpusLogic`) y ajustando sus espacios de nombres (`namespace`) para una correcta integración.
3.  **Diseño de la Interfaz de Usuario (UI):**
    *   Se utilizó un `TableLayoutPanel` para representar visualmente la cuadrícula del laberinto.
    *   Se añadieron controles `Label` dinámicamente a cada celda del `TableLayoutPanel` para mostrar el contenido de cada habitación (jugador, pistas, elementos del juego).
    *   Se incluyó un `Label` adicional para mostrar mensajes de estado (pistas, resultado del juego).
    *   Se agregó un `Button` para la funcionalidad de "Nuevo Juego".
4.  **Conexión UI-Lógica:**
    *   El código en `Form1.cs` se encarga de instanciar la clase `Juego`.
    *   Los eventos de teclado (W,A,S,D) se capturan para invocar los métodos de movimiento en la instancia de `Juego`.
    *   Después de cada acción del jugador o al iniciar el juego, un método `RenderBoard()` actualiza el texto y los colores de los `Label` en el `TableLayoutPanel` para reflejar el estado actual del `Laberinto` y la posición del `Jugador`.
    *   Los mensajes de estado y los resultados del juego se muestran en el `Label` de estado.

Este enfoque permitió reutilizar la lógica de juego ya probada y centrarse en la implementación de la capa de presentación visual.
