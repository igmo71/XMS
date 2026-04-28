# AI Task Template

## Context

Use:
- /docs/ai/architecture.md
- /docs/ai/coding-rules.md

## Task

Describe the exact change here.

Example:
Refactor duplicated startup configuration from XMS.Api and XMS.Web into shared hosting extension methods.

## Scope

Allowed to modify:
- list files or folders

Do not modify:
- list files or folders

## Requirements

- Keep existing behavior.
- Do not introduce new third-party dependencies.
- Do not change public API unless explicitly required.
- Do not move app-specific configuration into shared infrastructure.
- Code must compile.

## Expected Output

- List changed files.
- Provide final code changes.
- Explain why the change is safe.
- Mention any assumptions.
- Mention any follow-up recommendations.

## Review Checklist

- Does the code respect project boundaries?
- Is duplication reduced without hiding too much behind magic?
- Are consumers/background services registered only where intended?
- Are names clear?
- Is the change minimal?
