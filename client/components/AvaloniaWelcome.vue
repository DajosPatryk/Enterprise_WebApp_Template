<script setup lang="ts">
const asyncRes = ref({});
const res = ref({});
const asyncTimer = ref('');
const timer = ref('');

// Fetches data internally before page render.
asyncRes.value = await useAsyncApi('/User?sub=1');

// Fetches data externally after page rendered, on func run.
async function createUser(sub: string, email: string): Promise<void>{
  const startTime = performance.now();

  res.value = await useApi('/User', {
    method: 'POST',
    body: {
      user: {
        sub,
        email
      }
    }
  });

  const endTime = performance.now();
  const duration = endTime - startTime;
  if (duration < 1000) timer.value = `${Math.round(duration)}ms`;
  else timer.value = `${(duration / 1000).toFixed(1)}s`;
}
async function updateUser(sub: string, email: string): Promise<void>{
  const startTime = performance.now();

  res.value = await useApi('/User', {
    method: 'PATCH',
    body: {
      user: {
        sub,
        email
      }
    }
  });

  const endTime = performance.now();
  const duration = endTime - startTime;
  if (duration < 1000) timer.value = `${Math.round(duration)}ms`;
  else timer.value = `${(duration / 1000).toFixed(1)}s`;
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

    <div class="mb-2 mt-12">
      <span class="bg-util-800 text-util-500 rounded-sm p-2 text-xs">This is SSR connection test, it gets first user in Database before render. <span class="font-semibold">Connecting to API internally.</span></span>
    </div>
    <div class="flex justify-center items-center mb-12">
      <div v-if="asyncRes.value">
        <div>
          <span class="code cd-section">{{ asyncRes.value }}</span>
        </div>
      </div>
    </div>

    <Button
        class="mr-2"
        @click="createUser('1',  `${Array(10).fill(null).map(() => Math.random().toString(36)[2]).join('')}@${Array(5).fill(null).map(() => Math.random().toString(36)[2]).join('')}.space`)"
    >
      <span>Create User</span>
    </Button>
    <Button
        @click="updateUser('1',  `${Array(10).fill(null).map(() => Math.random().toString(36)[2]).join('')}@${Array(5).fill(null).map(() => Math.random().toString(36)[2]).join('')}.space`)"
    >
      <span>Update User</span>
    </Button>
    <div class="mt-2">
      <span class="bg-util-800 text-util-500 rounded-sm p-2 text-xs">This test creates or updates a user in Database. <span class="font-semibold">Connecting to API externally.</span></span>
    </div>

    <div class="flex justify-center items-center my-12">
      <div v-if="res.value">
        <h5 class="text-primary-500">
          {{ res.status }} - {{ res.statusText }}
        </h5>
        <span class="mt-2 bg-util-800 text-util-500 rounded-sm p-2 text-xs">{{ `${timer} processing time`}}</span>
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

.code.cd-section {
  border-left: 3px solid var(--primary-500);
  page-break-inside: avoid;
  font-family: monospace;
  font-size: 15px;
  line-height: 1.6;
  max-width: 100%;
  overflow: auto;
  display: block;
  word-wrap: break-word;
  padding: 10px 20px;
}
</style>