function createPhaseRow(phaseName, phaseOrder) {
    const tableBody = document.querySelector(".table-scroll table tbody");
    const newRow = document.createElement("tr");

    const nameTd = document.createElement("td");
    const nameInput = document.createElement("input");
    nameInput.type = "text";
    nameInput.classList.add("form-control");
    nameInput.name = `AddCompetitionViewModel.Phases[${tableBody.children.length}].Libelle`;
    nameInput.value = phaseName;
    nameTd.appendChild(nameInput);

    const orderTd = document.createElement("td");
    const orderInput = document.createElement("input");
    orderInput.type = "number";
    orderInput.classList.add("form-control");
    orderInput.name = `AddCompetitionViewModel.Phases[${tableBody.children.length}].Ordre`;
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

function deletePhase(buttonElement) {
    var row = buttonElement.closest('tr');
    row.parentNode.removeChild(row);
}

function deleteBtnOnClick(buttonElement) {
    deletePhase(buttonElement);
}

function deleteAllPhases() {
    const tableBody = document.querySelector(".table-scroll table tbody");
    tableBody.innerHTML = "";
}