<template>
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
</template>

<script setup>
  import {ref} from "vue";

  const props = defineProps({
    form: Object
  })

  const loading = ref(false)
  const filteredEmployees = ref([])
  
  function searchEmployees(target, query) {
    loading.value = true

    if (!query || query.trim() === '') {
      filteredEmployees.value = [];
      loading.value = false;
      return;
    }

    let list = target === 'client'
        ? props.form.clientCompanyStaff
        : props.form.contractorCompanyStaff

    filteredEmployees.value = list.filter(e =>
        e.name.toLowerCase().includes(query.toLowerCase()))

    loading.value = false
  }
  //TODO
</script>
