<template>
  <el-steps :active="activeStep" finish-status="success" class="mb-4">
    <el-step v-for="(step, index) in steps" :key="index" :title="step" />
  </el-steps>

  <div v-if="activeStep === 0">
    <el-form :model="form">
      <el-form-item label="Название проекта">
        <el-input v-model="form.projectName" />
      </el-form-item>
      <el-form-item label="Дата начала">
        <el-date-picker v-model="form.startDate" type="date" />
      </el-form-item>
      <el-form-item label="Дата окончания">
        <el-date-picker v-model="form.endDate" type="date" />
      </el-form-item>
      <el-form-item label="Приоритет">
        <el-select v-model="form.priority" placeholder="Выберите приоритет">
          <el-option label="Высокий" value="3" />
          <el-option label="Средний" value="2" />
          <el-option label="Низкий" value="1" />
        </el-select>
      </el-form-item>
    </el-form>
  </div>

  <div v-if="activeStep === 1">
    <el-form-item label="Client Company">
      <el-input v-model="form.clientCompany" />
    </el-form-item>

    <el-form-item label="Add an employee">
      <el-input v-model="newClientEmployee.name" placeholder="Name" style="margin-bottom: 6px" />
      <el-input v-model="newClientEmployee.speciality" placeholder="Speciality" style="margin-bottom: 6px" />
      <el-button type="primary" @click="addEmployee('client')">Add</el-button>
    </el-form-item>

    <el-table :data="form.clientCompanyStaff" style="width: 100%; margin-bottom: 20px">
      <el-table-column prop="name" label="Name">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editName" />
          </template>
          <template v-else>{{ row.name }}</template>
        </template>
      </el-table-column>

      <el-table-column prop="speciality" label="Speciality">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editSpeciality" />
          </template>
          <template v-else>{{ row.speciality }}</template>
        </template>
      </el-table-column>

      <el-table-column label="Actions" width="160">
        <template #default="{ row }">
          <el-button
              v-if="row.isEditing"
              size="small"
              type="success"
              @click="saveEmployee(row)"
          >Save</el-button>
          <el-button
              v-else
              size="small"
              @click="editEmployee(row)"
          >Change</el-button>
          <el-button size="small" type="danger" @click="deleteEmployee('client', row.id)">Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>

  <div v-if="activeStep === 2">
    <el-form-item label="Contractor Company">
      <el-input v-model="form.contractorCompany" />
    </el-form-item>

    <el-form-item label="Add an employee">
      <el-input v-model="newContractorEmployee.name" placeholder="Name" style="margin-bottom: 6px" />
      <el-input v-model="newContractorEmployee.speciality" placeholder="Speciality" style="margin-bottom: 6px" />
      <el-button type="primary" @click="addEmployee('contractor')">Add</el-button>
    </el-form-item>

    <el-table :data="form.contractorCompanyStaff" style="width: 100%; margin-bottom: 20px">
      <el-table-column prop="name" label="Name">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editName" />
          </template>
          <template v-else>{{ row.name }}</template>
        </template>
      </el-table-column>

      <el-table-column prop="speciality" label="Speciality">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editSpeciality" />
          </template>
          <template v-else>{{ row.speciality }}</template>
        </template>
      </el-table-column>

      <el-table-column label="Actions" width="160">
        <template #default="{ row }">
          <el-button
              v-if="row.isEditing"
              size="small"
              type="success"
              @click="saveEmployee(row)"
          >Save</el-button>
          <el-button
              v-else
              size="small"
              @click="editEmployee(row)"
          >Change</el-button>
          <el-button size="small" type="danger" @click="deleteEmployee('contractor', row.id)">Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>

  <div v-if="activeStep === 3">
    <el-form-item label="Performers">
      <el-select
          v-model="form.performersId"
          filterable
          multiple
          remote
          placeholder="Search by name"
          :remote-method="(query) => searchEmployees('contractor', query)"
          :loading="loading"
      >
        <el-option
            v-for="employee in filteredEmployees"
            :key="employee.id"
            :label="employee.name"
            :value="employee.id"
        />
      </el-select>
    </el-form-item>
    
    <el-table :data="form.performersId">
      <el-table-column prop="name" label="Name">
        <template #default="{ row }">
          {{form.contractorCompanyStaff.find(e => e.id === row)?.name}}
        </template>  
      </el-table-column>

      <el-table-column prop="speciality" label="Speciality">
        <template #default="{ row }">
          {{form.contractorCompanyStaff.find(e => e.id === row)?.speciality}}
        </template>
      </el-table-column>

      <el-table-column label="Actions" width="160">
        <template #default="{ row }">
          <el-button size="small" type="danger" 
                     @click="
                       form.performersId = form.performersId.filter(id => id !== row)
                     ">Delete</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>

  <div v-if="activeStep === 4">
    <el-form-item label="Manager">
      <el-select
          v-model="form.managerId"
          filterable
          remote
          reserve-keyword
          placeholder="Search by name"
          :remote-method="(query) => searchEmployees('contractor', query)"
          :loading="loading"
      >
        <el-option
              v-for="employee in filteredEmployees"
            :key="employee.id"
            :label="employee.name"
            :value="employee.id"
        />
      </el-select>
      <div v-if="form.managerId">
        <p>Chosen manager:
          {{ form.contractorCompanyStaff.find(e => e.id === form.managerId)?.name }}
        </p>
      </div>
    </el-form-item>
  </div>

  <div v-if="activeStep === 5">
    <div
        class="upload-container"
        @dragover.prevent="handleDragOver"
        @dragleave.prevent="handleDragLeave"
        @drop.prevent="handleDrop"
    >
      <div
          class="upload-box"
          :class="{ 'is-dragging': isDragging }"
          @click="triggerFileInput"
      >
        <input
            type="file"
            multiple
            ref="fileInput"
            class="file-input"
            @change="handleFileChange"
        />
        <i class="el-icon-upload" />
        <div class="upload-text">
          Drag your files here or <em>click to choose</em>
        </div>
      </div>
      <div v-if="fileList.length > 0">
        <h3>Uploaded Files:</h3>
        <ul>
          <li v-for="(file, index) in fileList" :key="file.name + index">
            {{ file.name }} - {{ file.size }} bytes
            <el-button size="small" type="danger" @click="removeFile(index)">Remove</el-button>
          </li>
        </ul>
      </div>
    </div>
  </div>

  <div class="mt-4 flex justify-between">
    <el-button @click="prevStep" :disabled="activeStep === 0">Back</el-button>
    <el-button v-if="activeStep < 5" type="primary" @click="nextStep">Next</el-button>
    <el-button v-if="activeStep === 5" type="primary" @click="UploadForm">Finish</el-button>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import {v4 as uuidv4} from 'uuid'

const activeStep = ref(0)
const steps = ['Project Information', 'Client Company', 'Contractor Company', 'Performers', 'Supervisor selection', 'Documents']

const form = ref({
  projectName: '',
  startDate: '',
  endDate: '',
  priority: '',
  clientCompany: '',
  contractorCompany: '',
  managerId: '',
  performersId: [],
  clientCompanyStaff: [],
  contractorCompanyStaff: []
})

const apiUrl = import.meta.env.VITE_API_URL

const UploadForm = async () => 
{
  const companiesIds = await UploadCompaniesAsync()
  
  await UploadEmployeesAsync(companiesIds)
  
  const projectId = await UploadProjectAsync(companiesIds)
  
  await UploadFilesAsync(projectId)
}

async function UploadFilesAsync(projectId)
{
  const formData = new FormData()
  
  formData.append('projectId', projectId)
  
  fileList.value.forEach(file => {
    formData.append('files', file)
  })

  await axios.post(`${apiUrl}/api/documents/bulk`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}
async function UploadProjectAsync(companiesIds)
{
  const projectId = uuidv4()
  const project =
      {
        Id: projectId,
        Name: form.value.projectName,
        StartDate: form.value.startDate,
        EndDate: form.value.endDate,
        Priority: form.value.priority,
        ClientCompanyId: companiesIds[0],
        ContractorCompanyId: companiesIds[1], 
        SupervisorId: form.value.managerId,
        EmployeeProjects: form.value.performersId.map(id => ({
          EmployeeId: id,
          ProjectId: projectId
        }))
      }
  
  await axios.post(`${apiUrl}/api/projects`, project)
  
  return projectId
}

async function UploadEmployeesAsync(companiesResponse) {
  const clientEmployees = form.value.clientCompanyStaff.map(employee => {
    return {
      Id: employee.id,
      Name: employee.name,
      Speciality: employee.speciality,
      CompanyId: companiesResponse[0]
    }
  })

  const contractorEmployees = form.value.contractorCompanyStaff.map(employee => {
    return {
      Id: employee.id,
      Name: employee.name,
      Speciality: employee.speciality,
      CompanyId: companiesResponse[1]
    }
  })
  
  const clientEmployeesResponse = await axios.post(`${apiUrl}/api/employees/bulk`, clientEmployees)
  const contractorEmployeesResponse = await axios.post(`${apiUrl}/api/employees/bulk`, contractorEmployees)
}

async function UploadCompaniesAsync() {
  let clCompany =
      {
        Id: uuidv4(),
        Name: form.value.clientCompany
      }

  let contCompany =
      {
        Id: uuidv4(),
        Name: form.value.contractorCompany
      }
  
  await axios.post(`${apiUrl}/api/companies`, clCompany)
  await axios.post(`${apiUrl}/api/companies`, contCompany)

  return [clCompany.Id, clCompany.Id]
}


const fileList = ref([])
const isDragging = ref(false)
const fileInput = ref(null)

const removeFile = (index) => {
  fileList.value.splice(index, 1)
}

function handleFileChange(event) {
  const newFiles = Array.from(event.target.files)
  fileList.value = [...fileList.value, ...newFiles]
  event.target.value = ''
}

function handleDragOver(event) {
  isDragging.value = true;
}

function handleDragLeave() {
  isDragging.value = false;
}

function handleDrop(event) {
  const files = event.dataTransfer.files;
  fileList.value = Array.from(files);
  isDragging.value = false;
}

function triggerFileInput() {
  fileInput.value?.click()
}

const loading = ref(false)

const newClientEmployee = ref({ name: '', speciality: '' })
const newContractorEmployee = ref({ name: '', speciality: '' })


function addEmployee(target) {
  const employee = {
    id: uuidv4(),
    name: target === 'client' ? newClientEmployee.value.name : newContractorEmployee.value.name,
    speciality: target === 'client' ? newClientEmployee.value.speciality : newContractorEmployee.value.speciality,
    isEditing: false,
  }

  if (target === 'client') {
    form.value.clientCompanyStaff.push(employee)
    newClientEmployee.value = { name: '', speciality: '' }
  } else {
    form.value.contractorCompanyStaff.push(employee)
    newContractorEmployee.value = { name: '', speciality: '' }
  }
}

function editEmployee(emp) {
  emp.isEditing = true
  emp.editName = emp.name
  emp.editSpeciality = emp.speciality
}
function saveEmployee(emp) {
  emp.name = emp.editName
  emp.speciality = emp.editSpeciality
  emp.isEditing = false
}

const filteredEmployees = ref([])

function searchEmployees(target, query) {
  loading.value = true

  if (!query || query.trim() === '') {
    filteredEmployees.value = [];
    loading.value = false;
    return;
  }
  
  let list = target === 'client'
  ? form.value.clientCompanyStaff
  : form.value.contractorCompanyStaff
  
  filteredEmployees.value = list.filter(e =>
  e.name.toLowerCase().includes(query.toLowerCase()))
  
  loading.value = false
}

function deleteEmployee(target, id) {
  if(target === 'client') {
    form.value.clientCompanyStaff = form.value.clientCompanyStaff.filter(e => e.id !== id)
  }
  if(target === 'contractor') {
    form.value.contractorCompanyStaff = form.value.contractorCompanyStaff.filter(e => e.id !== id)
  }
}

const nextStep = () => {
  if (activeStep.value < steps.length - 1) activeStep.value++
}
const prevStep = () => {
  if (activeStep.value > 0) activeStep.value--
}
</script>

<style scoped>
.upload-demo {
  border: 2px dashed #d9d9d9;
  border-radius: 6px;
  padding: 40px;
  text-align: center;
  background-color: #f9f9f9;
}
.upload-container {
  width: 100%;
  max-width: 500px;
  margin: 0 auto;
  text-align: center;
  padding: 20px;
}

.upload-box {
  width: 100%;
  border: 2px dashed #cccccc;
  padding: 40px;
  cursor: pointer;
  transition: border-color 0.3s, background-color 0.3s;
}

.upload-box.is-dragging {
  border-color: #4caf50;
  background-color: #e8f5e9;
}

.upload-box:hover {
  border-color: #4caf50;
}

.upload-box .el-icon-upload {
  font-size: 40px;
  color: #4caf50;
  margin-bottom: 10px;
}

.upload-text {
  font-size: 16px;
  color: #888;
}

.upload-text em {
  color: #4caf50;
}

.file-input {
  display: none;
}

ul {
  list-style-type: none;
  padding: 0;
}

ul li {
  font-size: 14px;
  color: #333;
  padding: 5px 0;
}
</style>
