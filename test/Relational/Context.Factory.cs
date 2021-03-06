﻿namespace Microsoft.EntityFrameworkCore.Tests
{
    public abstract class RelationalContextFactoryBase<TContext> :
        ContextFactoryBase<TContext>
        where TContext : DbContext
    {
        protected override void PostConfigure(DbContextOptionsBuilder optionsBuilder)
        {
            base.PostConfigure(optionsBuilder);
            optionsBuilder.AddInterceptors(new CommandInterceptor(CommandTracer));
        }
    }
}
