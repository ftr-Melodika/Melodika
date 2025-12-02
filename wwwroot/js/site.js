// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function seleccionarInstrumento(instrumento) {
        // Aquí puedes agregar la lógica para manejar la selección del instrumento
        console.log('Instrumento seleccionado:', instrumento);
        // Ejemplo de redirección (ajusta según tu ruta)
        // window.location.href = `/Aprender/${instrumento}`;
        
        // Muestra un mensaje temporal (puedes personalizar esto)
        const mensaje = `¡Has seleccionado ${instrumento.charAt(0).toUpperCase() + instrumento.slice(1)}!`;
        alert(mensaje);
    }

// Toggle menu function
function toggleMenu() {
    // Add menu toggle functionality here
    console.log("Menu toggled");
    // You can add your menu toggle logic here
}

// Chord data (string: [fret, finger])
const chords = {
    'G': {
        '6': [3, 3],  // Low E string, 3rd fret, finger 3
        '5': [2, 2],  // A string, 2nd fret, finger 2
        '1': [3, 1]   // High E string, 3rd fret, finger 1
    },
    'B7': {
        '5': [2, 1],  // A string, 2nd fret, finger 1
        '4': [1, 1],  // D string, 1st fret, finger 1
        '3': [2, 2],  // G string, 2nd fret, finger 2
        '1': [2, 3],  // High E string, 2nd fret, finger 3
        '6': ['x']    // Low E string, muted
    },
    'Em': {
        '5': [2, 1],  // A string, 2nd fret, finger 1
        '4': [2, 2]   // D string, 2nd fret, finger 2
    },
    'C': {
        '5': [3, 3],  // A string, 3rd fret, finger 3
        '4': [2, 2],  // D string, 2nd fret, finger 2
        '2': [1, 1],  // B string, 1st fret, finger 1
        '6': ['x']    // Low E string, muted
    },
    'D': {
        '3': [2, 2],  // G string, 2nd fret, finger 2
        '2': [3, 3],  // B string, 3rd fret, finger 3
        '1': [2, 1],  // High E string, 2nd fret, finger 1
        '6': ['x'],   // Low E string, muted
        '5': ['x']    // A string, muted
    }
};

// Song structure - only the intro part
const songChords = ['G', 'B7', 'Em', 'C', 'G', 'D'];
let currentChordIndex = 0;
let isPlaying = false;
let playbackInterval;

// Function to show chord on the fretboard
function showChord(chordName) {
    // Clear all active and muted classes
    document.querySelectorAll('.fret').forEach(fret => {
        fret.classList.remove('active', 'muted');
        fret.textContent = '';
    });

    // Update current chord display
    const chordNameElement = document.getElementById('currentChord');
    if (chordNameElement) {
        chordNameElement.textContent = chordName;
    }

    const chord = chords[chordName];
    if (!chord) return;

    // Set active and muted frets
    for (const [string, data] of Object.entries(chord)) {
        const [fret, finger] = data;
        const fretElement = document.querySelector(`.fret[data-string="${string}"][data-fret="${fret}"]`);
        
        if (fret === 'x') {
            // Muted string
            const openFret = document.querySelector(`.fret[data-string="${string}"][data-fret="0"]`);
            if (openFret) {
                openFret.classList.add('muted');
            }
        } else if (fretElement) {
            // Active fret with finger number
            fretElement.classList.add('active');
            fretElement.textContent = finger;
        }
    }
}

// Function to play next chord
function playNextChord() {
    if (songChords.length === 0) return;
    
    showChord(songChords[currentChordIndex]);
    currentChordIndex = (currentChordIndex + 1) % songChords.length;
}

// Toggle playback
function togglePlayback() {
    const playButton = document.getElementById('playButton');
    if (!playButton) return;
    
    if (isPlaying) {
        clearInterval(playbackInterval);
        playButton.textContent = '▶️ Reproducir';
    } else {
        // If we're at the end of the song, start over
        if (currentChordIndex >= songChords.length) {
            currentChordIndex = 0;
        }
        playButton.textContent = '⏸️ Pausar';
        playbackInterval = setInterval(playNextChord, 2000); // Change chord every 2 seconds
    }
    
    isPlaying = !isPlaying;
}

// Event listeners when the DOM is fully loaded
document.addEventListener('DOMContentLoaded', () => {
    // Show first chord by default
    if (songChords.length > 0) {
        showChord(songChords[0]);
    }

    // Play button
    const playButton = document.getElementById('playButton');
    if (playButton) {
        playButton.addEventListener('click', togglePlayback);
    }
    
    // Next button
    const nextButton = document.getElementById('nextButton');
    if (nextButton) {
        nextButton.addEventListener('click', () => {
            if (isPlaying) {
                togglePlayback(); // Pause if playing
            }
            playNextChord();
        });
    }
});