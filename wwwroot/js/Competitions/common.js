const phasesStartEnum = {
    2: "Finale",
    4: "Demi-finale",
    8: "Quart de finale",
    16: "8ème de finale",
    32: "16ème de finale"
};

document.addEventListener("DOMContentLoaded", function () {
    initCompetition();

    document.querySelector('#championshipRadio').addEventListener('change', toggleCompetitionType);
    document.querySelector('#tournamentRadio').addEventListener('change', toggleCompetitionType);
    document.querySelector('#addNewPhase').addEventListener('click', addNewPhase);
});

function toggleCompetitionType() {
    let championshipFields = document.querySelector('#championshipFields');
    let tournamentFields = document.querySelector('#tournamentFields');

    if (document.querySelector('#championshipRadio').checked) {
        championshipFields.classList.remove('hidden');
        tournamentFields.classList.add('hidden');
    } else {
        championshipFields.classList.add('hidden');
        tournamentFields.classList.remove('hidden');
    }
}

function initCompetition() {
    document.querySelector('#championshipRadio').checked = true;
    toggleCompetitionType();

    const selectPhase = document.querySelector('#tournament-start-phase');
    const phasesStartArray = convertEnumToArray(phasesStartEnum).reverse();

    for (const [value, text] of phasesStartArray) {
        const optionElement = document.createElement("option");
        optionElement.value = value;
        optionElement.textContent = text;
        selectPhase.appendChild(optionElement);
    }
}

function convertEnumToArray(enumObj) {
    return Object.keys(enumObj).map(key => [parseInt(key), enumObj[key]]);
}

function generateChampionship() {
    const teams = parseInt(document.querySelector('#championship-teams').value);
    const directConfrontations = parseInt(document.querySelector('#championship-matches').value);

    if (isNaN(teams) || isNaN(directConfrontations)) {
        alert("Veuillez fournir le nombre d'équipes et de confrontations directes.");
        return;
    }

    const days = (teams - 1) * directConfrontations;
    let phases = [];

    for (let i = 1; i <= days; i++) {
        phases.push(i === 1 ? '1ère journée' : `${i}ème journée`);
    }
    populatePhaseInputs(phases)
}

function generateTournament() {
    let phases = [];
    const teamsPerGroup = parseInt(document.querySelector('#tournament-teams-per-group').value);
    const matchesPerTeam = parseInt(document.querySelector('#tournament-matches-per-team').value);
    const startPhase = parseInt(document.querySelector('#tournament-start-phase').value);
    const returnMatch = document.querySelector('#tournament-return-match').checked;

    const groupDays = (teamsPerGroup - 1) * matchesPerTeam;

    for (let i = 1; i <= groupDays; i++) {
        phases.push(i === 1 ? '1ère journée' : `${i}ème journée`);
    }

    for (let i = startPhase; i >= 2; i /= 2) {
        const baseValue = phasesStartEnum[i];

        if (i !== 2 && returnMatch) {
            phases.push(baseValue + " Aller");
            phases.push(baseValue + " Retour");
        } else {
            phases.push(baseValue);
        }
    }
    populatePhaseInputs(phases)
}

function populatePhaseInputs(phases) {
    const tableBody = document.querySelector(".table-scroll table tbody");
    tableBody.innerHTML = "";

    phases.forEach((phase, index) => {
        const newRow = createPhaseRow(phase, index + 1);
        tableBody.appendChild(newRow);
    });

    const divTablePhases = document.getElementById('phasesTableDiv');
    divTablePhases.scrollTop = divTablePhases.scrollHeight;
}

function addNewPhase() {
    const tableBody = document.querySelector(".table-scroll table tbody");
    const lastOrderInput = tableBody.querySelector('tbody tr:last-child td input[name$=".Ordre"]');
    let nextOrder = 1;

    if (lastOrderInput) {
        nextOrder = parseInt(lastOrderInput.value) + 1;
    }

    const newRow = createPhaseRow("Nouvelle Phase", nextOrder);
    tableBody.appendChild(newRow);

    const divTablePhases = document.getElementById('phasesTableDiv');
    divTablePhases.scrollTop = divTablePhases.scrollHeight;
}
