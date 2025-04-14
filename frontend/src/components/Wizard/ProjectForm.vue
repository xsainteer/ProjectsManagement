<template>
  <div>
    <el-form :model="form">
      <el-form-item label="Project Name">
        <el-input v-model="form.projectName" />
      </el-form-item>
      <el-form-item label="Start Date">
        <el-date-picker v-model="form.startDate" type="date" />
      </el-form-item>
      <el-form-item label="End Date">
        <el-date-picker v-model="form.endDate" type="date" />
      </el-form-item>
      <el-form-item label="Priority">
        <el-select v-model="form.priority" placeholder="Select Priority">
          <el-option label="Высокий" value="3" />
          <el-option label="Средний" value="2" />
          <el-option label="Низкий" value="1" />
        </el-select>
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup>
const props = defineProps({
  form: Object
});
</script>
