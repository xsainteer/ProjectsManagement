const inputs = document.querySelectorAll("input");


//logic, that sets all the input data in localStorage
inputs.forEach((input) => {
    
    if(input.closest("div").id === "ClientCompanyEmployeesForm" ||
        input.closest("div").id === "ExecutorCompanyEmployeesForm")
    {
        return;
    }
    
    input.addEventListener("input", (e) => {
        localStorage.setItem(input.id, input.value);
    })
});

function loadEmployees() {
    const employees = JSON.parse(localStorage.getItem('employees')) || [];
    const employeesContainer = document.getElementById('employeesContainer');
    employeesContainer.innerHTML = ''; 

    employees.forEach(employee => {
        const employeeDiv = document.createElement('div');
        employeeDiv.textContent = `Name: ${employee.name}, Speciality: ${employee.speciality}`;
        employeesContainer.appendChild(employeeDiv);
    });
}

//TODO right now the whole list of employees is loaded when adding or displaying a new employee, maybe i'll fix that

document.getElementById('addClientEmployee').addEventListener('click', () => {
    const name = document.getElementById('ClientEmployeeName').value;
    const speciality = document.getElementById('ClientEmployeeSpeciality').value;

    const clientEmployees = JSON.parse(localStorage.getItem('clientEmployees')) || [];
    
    clientEmployees.push({ name, speciality });
    
    localStorage.setItem('clientEmployees', JSON.stringify(employees));
    
    loadEmployees();
    
    document.getElementById('ClientEmployeeName').value = '';
    document.getElementById('ClientEmployeeSpeciality').value = '';
});

document.getElementById('addClientEmployee').addEventListener('click', () => {
    const name = document.getElementById('ClientEmployeeName').value;
    const speciality = document.getElementById('ClientEmployeeSpeciality').value;

    const clientEmployees = JSON.parse(localStorage.getItem('clientEmployees')) || [];

    clientEmployees.push({ name, speciality });

    localStorage.setItem('clientEmployees', JSON.stringify(employees));

    loadEmployees();

    document.getElementById('ClientEmployeeName').value = '';
    document.getElementById('ClientEmployeeSpeciality').value = '';
});

window.addEventListener('load', loadEmployees);

