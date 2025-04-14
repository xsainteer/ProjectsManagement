<template>
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
</template>

<script setup>
import { ref } from "vue";

const props = defineProps({
  form: Object,
})

let contractorEmployeeCounter = 1

const newContractorEmployee = ref({ name: '', speciality: '' })

function addEmployee(target) {
  const employee = {
    id: contractorEmployeeCounter,
    name: newContractorEmployee.value.name,
    speciality: newContractorEmployee.value.speciality,
    isEditing: false,
  }

  form.value.contractorCompanyStaff.push(employee)
  contractorEmployeeCounter += 1
  newContractorEmployee.value = { name: '', speciality: '' }
}

function deleteEmployee(target, id) {
  form.value.contractorCompanyStaff = form.value.contractorCompanyStaff.filter(e => e.id !== id)
}

function saveEmployee(emp) {
  emp.name = emp.editName
  emp.speciality = emp.editSpeciality
  emp.isEditing = false
}

function editEmployee(emp) {
  emp.isEditing = true
  emp.editName = emp.name
  emp.editSpeciality = emp.speciality
}
</script>
