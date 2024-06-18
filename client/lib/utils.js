import { clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'

export function cn(...inputs) {
  return twMerge(clsx(inputs))
}

/**
 * Extracts and transforms relevant fields from a response object, including asynchronously parsing
 * the JSON content of the response.
 *
 * @param {object} response - An object containing a nested `value` object. This nested object should
 * include properties `ok`, `status`, `statusText`, and a `json` method for parsing the response body.
 * @returns {Promise<{ ok: boolean, status: string, statusText: string, value: Object }>} A promise that resolves to an object containing the properties:
 *  - `ok` (boolean): Indicates whether the response was successful.
 *  - `status` (number): The HTTP status code of the response.
 *  - `statusText` (string): The status message corresponding to the status code.
 *  - `value` (any): The parsed JSON data from the response body.
 */
export async function cr(response) {
  let streamValue;
  try {
    streamValue = await response.value.json();
  } catch (error) {
    streamValue = {};
  }

  return {
    ok: response.value.ok,
    status: response.value.status,
    statusText: response.value.statusText,
    ...(streamValue ? { value: streamValue } : {})
  }
}

/**
 * Generates fetch options for an HTTP request, optionally including authorization headers.
 * Merges additional options and headers provided by the caller.
 *
 * @param {Object} [options={ headers: {} }] - The base options for the fetch request, which may include headers and other fetch settings.
 * @param {boolean} [includeAuthorization=false] - If true, includes authorization headers in the fetch options.
 * @returns {Promise<Object>} A promise that resolves to the configured fetch options, suitable for use with fetch API.
 */
export async function fo(options = { headers: {} }, includeAuthorization = false) {
  if (includeAuthorization) {
    const token = ''; // TODO: Place access token here.
    var authHeader = {
      Authorization: `Bearer ${token}`
    };
  }

  const fetchOptions = {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      ...(includeAuthorization ? authHeader : {}),
      ...options.headers,
    },
    ...options,
  };
  fetchOptions.body = fetchOptions.body ? JSON.stringify(fetchOptions.body) : undefined;

  return fetchOptions;
}
