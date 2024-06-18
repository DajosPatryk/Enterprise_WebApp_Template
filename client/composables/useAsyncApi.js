import {fo} from "~/lib/utils.js";

/**
 * Custom composable function to perform API requests using a predefined base URL and headers.
 * This function is designed for server-side rendering (SSR) to ensure data is fetched before the page is rendered.
 * It utilizes Nuxt's useAsyncData to handle asynchronous data fetching in a reactive and SSR-friendly manner.
 *
 * @param {string} endpoint - The API endpoint to which the request will be made, appended to the base URL.
 * @param {object} [options={}] - Optional configuration options for the fetch request, which may include HTTP method, body, and additional headers.
 *                                The default method is 'GET', and the 'Content-Type' header is set to 'application/json' by default.
 *                                These defaults can be overridden in the options object.
 * @param {boolean} [includeAuthorization=false] - Determines whether to include authorization headers in the request.
 * @param {object} asyncDataOptions - Configuration options for useAsyncData, which manages the async fetch operation.
 *                                                       See more at: https://nuxt.com/docs/api/composables/use-async-data
 *
 * @returns {Promise<object>} A promise resolving to an object containing data and other metadata relevant for the client.
 */
export async function useAsyncApi(endpoint, options, includeAuthorization = false, asyncDataOptions) {
    const runtimeConfig = useRuntimeConfig()
    const fetchOptions  = await fo(options, includeAuthorization);

    const res = await useAsyncData(
        () => $fetch(`${runtimeConfig.public.internalApiAddress}${endpoint}`, fetchOptions),
        asyncDataOptions
    );

    console.log("useAsyncData Addr", `${runtimeConfig.public.internalApiAddress}${endpoint}`);
    console.log("useAsyncData", res);

    return res.data;
}
