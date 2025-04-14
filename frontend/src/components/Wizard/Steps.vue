<template>
  <el-steps :active="activeStep" finish-status="success" class="mb-4">
    <el-step v-for="(step, index) in steps" :key="index" :title="step" />
  </el-steps>
</template>

<script setup>
const props = defineProps({
  activeStep: Number,
  steps: Array
});
</script>