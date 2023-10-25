function createPhaseRow(phaseName, phaseOrder) {
    const tableBody = document.querySelector(".table-scroll table tbody");
    const newRow = document.createElement("tr");

    const nameTd = document.createElement("td");
    const nameInput = document.createElement("input");
    nameInput.type = "text";
    nameInput.classList.add("form-control");
    nameInput.name = `EditCompetitionViewModel.Phases[${tableBody.children.length}].Libelle`;
    nameInput.value = phaseName;
    nameTd.appendChild(nameInput);

    const orderTd = document.createElement("td");
    const orderInput = document.createElement("input");
    orderInput.type = "number";
    orderInput.classList.add("form-control");
    orderInput.name = `EditCompetitionViewModel.Phases[${tableBody.children.length}].Ordre`;
    orderInput.value = phaseOrder;
    orderTd.appendChild(orderInput);

    const deleteTd = document.createElement("td");
    const deleteBtn = document.createElement("button");
    deleteBtn.classList.add("btn", "btn-danger", "btn-sm");
    deleteBtn.onclick = function () {
        deleteBtnOnClick(this);
    };
    deleteBtn.innerHTML = "Supprimer";
    deleteBtn.type = "button";
    deleteTd.appendChild(deleteBtn);

    newRow.appendChild(nameTd);
    newRow.appendChild(orderTd);
    newRow.appendChild(deleteTd);

    return newRow;
}

function deletePhase(buttonElement, phaseId) {
    if (confirm('Êtes-vous sûr de vouloir supprimer cette phase?')) {
        var row = buttonElement.closest('tr');
        row.parentNode.removeChild(row);

        if (phaseId > 0) {
            deletePhaseFromServer(phaseId);
        }
    }
}

function deletePhaseFromServer(phaseId) {

    var token = document.querySelector('[name="__RequestVerificationToken"]').value;
    var competitionId = document.getElementById('CompetitionId').value;

    fetch(`/Competitions/Edit/${competitionId}?handler=DeletePhase&phaseId=${phaseId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "RequestVerificationToken": token
        }
    })
        .then(response => response.json())
        .then(data => {
            if (!data.success) {
                alert(data.message);
            }
        });
}

function deleteAllPhases() {
    if (confirm('Êtes-vous sûr de vouloir supprimer les phases ?')) {
        const tableBody = document.querySelector(".table-scroll table tbody");
        tableBody.innerHTML = "";
        deleteAllPhasesFromServer();
    }
}

function deleteAllPhasesFromServer() {

    var token = document.querySelector('[name="__RequestVerificationToken"]').value;
    var competitionId = document.getElementById('CompetitionId').value;

    fetch(`/Competitions/Edit/${competitionId}?handler=DeleteAllPhases&competitionId=${competitionId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "RequestVerificationToken": token
        }
    })
        .then(response => response.json())
        .then(data => {
            if (!data.success) {
                alert(data.message);
            }
        });
}

function deleteBtnOnClick(buttonElement) {
    deletePhase(buttonElement, 0);
}