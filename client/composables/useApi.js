import {useFetch} from '@vueuse/core'
import {cr, fo} from "~/lib/utils";

/**
 * Custom composable function to perform API requests using a predefined base URL and headers.
 *
 * @param {string} endpoint - The API endpoint to which the request will be made, appended to the base URL.
 * @param {object} [options={}] - Optional configuration options for the fetch request which may include HTTP method, body, and additional headers.
 *                               The default method is 'GET' and the 'Content-Type' header is set to 'application/json' by default.
 *                               These defaults can be overridden in the options object.
 * @param {boolean} [includeAuthorization=false] - Determines whether to include authorization headers in the request.
 *
 * @returns {Promise<{ok: boolean, status: string, statusText: string, value: object}>} A object containing for client important data.
 */
export async function useApi(endpoint, options, includeAuthorization = false) {
    const runtimeConfig = useRuntimeConfig()
    const fetchOptions = await fo(options, includeAuthorization);

    const res = await useFetch(
        `${runtimeConfig.public.apiAddress}${endpoint}`,
        fetchOptions
    ).json();

    return await cr(res.response);
}
