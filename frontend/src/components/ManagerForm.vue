<template>
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
</script>