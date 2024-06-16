<script setup lang="ts">
const res = ref({});

async function createUser(sub: string, email: string): Promise<void>{

}
</script>

<template>
  <div class="welcome py-16 text-center m-auto">
    <div class="md:py-16 py-8">
      <h1>Welcome to Avalonias Web-App Template</h1>
      <p>A modern performance oriented template for Enterprise-Apps.</p>
    </div>

    <div class="flex justify-center items-center">
      <div class="notice md:py-16 py-8 my-4">
        To get started remove <span class="code">{{ "<AvaloniaTesting/>" }}</span> from your <span class="code">pages/index.vue</span> file.
      </div>
    </div>

    <Button
        @click="createUser('1', 'patryk.dajos@avalonia.space')"
    >
      <span>Test DB Connection</span>
    </Button>

    <div class="flex justify-center items-center mt-12">
      <div>
        <h5 class="text-primary-500">
          200
        </h5>
        <div class="code mt-4 p-2">
          I am object
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="postcss">
.welcome {
  @apply bg-util-950;
  height: 100vh;
}

.notice {
  @apply bg-util-1000 p-6 rounded-lg border-2 border-primary-800;
}

.code {
  @apply bg-util-600 rounded-sm p-1;
}
</style>