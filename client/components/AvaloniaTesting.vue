<script setup lang="ts">
async function createUser(sub: string, email: string): Promise<void>{

}
</script>

<template>
  <div class="welcome">
    <h1>Welcome to Avalonias Web-App Template</h1>
    <span>E</span>
    <Button
        @click="createUser('1', 'patryk.dajos@avalonia.space')"
    >
      <span>Test DB Connection</span>
    </Button>
  </div>
</template>

<style scoped lang="postcss">
.welcome{
  height: 100vh;
}
</style>