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
          <el-option label="Высокий" value="high" />
          <el-option label="Средний" value="medium" />
          <el-option label="Низкий" value="low" />
        </el-select>
      </el-form-item>
    </el-form>
  </div>

  <div v-if="activeStep === 1">
    <el-form-item label="Компания-заказчик">
      <el-input v-model="form.clientCompany" />
    </el-form-item>

    <el-form-item label="Добавить сотрудника в заказчика">
      <el-input v-model="newClientEmployee.name" placeholder="Имя" style="margin-bottom: 6px" />
      <el-input v-model="newClientEmployee.speciality" placeholder="Должность" style="margin-bottom: 6px" />
      <el-button type="primary" @click="addEmployee('client')">Добавить</el-button>
    </el-form-item>

    <el-table :data="form.clientCompanyStaff" style="width: 100%; margin-bottom: 20px">
      <el-table-column prop="name" label="Имя">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editName" />
          </template>
          <template v-else>{{ row.name }}</template>
        </template>
      </el-table-column>

      <el-table-column prop="speciality" label="Должность">
        <template #default="{ row }">
          <template v-if="row.isEditing">
            <el-input v-model="row.editSpeciality" />
          </template>
          <template v-else>{{ row.speciality }}</template>
        </template>
      </el-table-column>

      <el-table-column label="Действия" width="160">
        <template #default="{ row }">
          <el-button
              v-if="row.isEditing"
              size="small"
              type="success"
              @click="saveEmployee(row)"
          >Сохранить</el-button>
          <el-button
              v-else
              size="small"
              @click="editEmployee(row)"
          >Изменить</el-button>
          <el-button size="small" type="danger" @click="deleteEmployee('client', row.id)">Удалить</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>


  <div v-if="activeStep === 2">
    <el-form-item label="Руководитель проекта">
      <el-select
          v-model="form.manager"
          filterable
          remote
          reserve-keyword
          placeholder="Поиск по имени"
          :remote-method="searchEmployees('contractor')"
          :loading="loading"
      >
        <el-option
            v-for="employee in employees"
            :key="employee.id"
            :label="employee.name"
            :value="employee.id"
        />
      </el-select>
    </el-form-item>
  </div>

  <div v-if="activeStep === 3">
    <el-form-item label="Исполнители проекта">
      <el-select
          v-model="form.performers"
          filterable
          multiple
          remote
          placeholder="Поиск сотрудников"
          :remote-method="searchEmployees"
          :loading="loading"
      >
        <el-option
            v-for="employee in employees"
            :key="employee.id"
            :label="employee.name"
            :value="employee.id"
        />
      </el-select>
    </el-form-item>
  </div>

  <div v-if="activeStep === 4">
    <el-upload
        class="upload-demo"
        drag
        action="https://your-upload-endpoint.com"
        multiple
    >
      <i class="el-icon-upload" />
      <div class="el-upload__text">Перетащите файлы сюда или <em>нажмите для выбора</em></div>
    </el-upload>
  </div>

  <div class="mt-4 flex justify-between">
    <el-button @click="prevStep" :disabled="activeStep === 0">Назад</el-button>
    <el-button type="primary" @click="nextStep">
      {{ activeStep === steps.length - 1 ? 'Готово' : 'Далее' }}
    </el-button>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const activeStep = ref(0)
const steps = ['Инфо о проекте', 'Компании', 'Руководитель', 'Исполнители', 'Документы']

const form = ref({
  projectName: '',
  startDate: '',
  endDate: '',
  priority: '',
  clientCompany: '',
  contractorCompany: '',
  manager: '',
  performers: [],
  clientCompanyStaff: [],
  contractorCompanyStaff: []
})


const employees = ref([])
const loading = ref(false)

const newClientEmployee = ref({ name: '', speciality: '' })
const newContractorEmployee = ref({ name: '', speciality: '' })

let clientEmployeeCounter = 0
let contractorEmployeeCounter = 0

function addEmployee(target) {
  const employee = {
    name: target === 'client' ? newClientEmployee.value.name : newContractorEmployee.value.name,
    speciality: target === 'client' ? newClientEmployee.value.speciality : newContractorEmployee.value.speciality,
    isEditing: false,
  }

  if (target === 'client') {
    form.value.clientCompanyStaff.push(employee)
    clientEmployeeCounter += 1
    newClientEmployee.value = { name: '', speciality: '' }
  } else {
    form.value.contractorCompanyStaff.push(employee)
    contractorEmployeeCounter += 1
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

function searchEmployees(target) {
  if (target === 'client') {
    return form.value.clientCompanyStaff
  } else if (target === 'contractor') {
    return form.value.contractorCompanyStaff
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
</style>
